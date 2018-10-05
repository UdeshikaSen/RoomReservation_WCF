using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RoomReservationService
{
    public class RoomReservationService : IRoomReservationService
    {
        private RoomReservationEntities entities = new RoomReservationEntities();
        
        public User GetUser(string userId)
        {
            try
            {
                User user = new User();
                return user.ConvertTo(entities.reservation_users.Single(u => u.email == userId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<User> GetUsers()
        {
            List<User> userList = new List<User>();
            try
            {
                List<reservation_user> entityUserList = entities.reservation_users.ToList();

                foreach (reservation_user entityUser in entityUserList)
                {
                    User user = new User();
                    userList.Add(user.ConvertTo(entityUser));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return userList;
        }

        public string RegisterUser(string userEmail, string userName, string userPhone, string userAddress)
        {
            string result = "";
            reservation_user user = new reservation_user()
            {
                email = userEmail,
                name = userName,
                phone = userPhone,
                user_address = userAddress
            };

            try
            {
                entities.reservation_users.Add(user);
                entities.SaveChanges();
                result = "Registered Successfully";
            }
            catch (Exception ex)
            // when (ex is OptimisticConcurrencyException || ex is UpdateException)
            {
                result = "Error occurred in saving the user to the DB";
                Console.WriteLine("Error in RegisterUser:" + ex.InnerException.InnerException.Message);
            }

            return result;
        }


        public List<Room> GetRooms()
        {
            List<Room> roomList = new List<Room>();
            try
            {
                List<room> entityRoomList = entities.rooms.ToList();

                foreach (room entityRoom in entityRoomList)
                {
                    Room room = new Room();
                    roomList.Add(room.ConvertTo(entityRoom));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return roomList;
        }


        public string ReserveRoom(string roomNo, int adults, int children, double reservationCost, string userEmail, DateTime inDateTime, DateTime outDateTime)
        {
            string reuslt = "";

            try
            {
                // update room status
                room roomToBeReserved = entities.rooms.Single(r => r.roomNo == roomNo);
                roomToBeReserved.roomState = "reserved";

                // save to reservation table and room_reservation table
                reservation newReservation = new reservation()
                {
                    reservationId = "R" + Guid.NewGuid().ToString("N"),
                    reservationDate = DateTime.Now,
                    noOfAdults = adults,
                    noOfChildren = children,
                    totalCost = reservationCost,
                    email = userEmail
                };

                entities.reservations.Add(newReservation);
                entities.SaveChanges();

                room_reservation roomReservation = new room_reservation()
                {
                    checkIn = inDateTime,
                    checkout = outDateTime,
                    reservationState = "tentative",
                    roomNo = roomNo,
                    reservationId = newReservation.reservationId
                };

                entities.room_reservations.Add(roomReservation);
                entities.SaveChanges();

                reuslt = "Reservation successfull";

            }
            catch (Exception ex)
            {
                reuslt = "Error occurred in saving the reservation" + ex.Message;
            }

            return reuslt;
        }

        public List<String> GetRoomTypes()
        {
            List<string> roomTypes = new List<string>();
            roomTypes.Add("All");

            foreach (string roomType in GetRooms().Select(r => r.RoomType).Distinct().ToList())
            {
                roomTypes.Add(roomType);
            }

            return roomTypes;
        }

        public List<Room> GetAvailableRooms(string roomType, DateTime checkIn, DateTime checkOut)
        {
            List<Room> roomList = new List<Room>();
            List<room> entityRoomList;

            try
            {
                if (roomType.ToLower().Equals("all"))
                {
                    entityRoomList = (from room in entities.rooms
                                      join room_reservation in entities.room_reservations on room.roomNo equals room_reservation.roomNo
                                      into rrr
                                      from x in rrr.DefaultIfEmpty()
                                      where x.reservationId == null || checkIn > x.checkout || checkOut < x.checkIn
                                      select room).ToList();
                }
                else
                {
                    entityRoomList = (from room in entities.rooms
                                      join room_reservation in entities.room_reservations on room.roomNo equals room_reservation.roomNo
                                      into rrr
                                      from x in rrr.DefaultIfEmpty()
                                      where room.roomType.Equals(roomType) && (x.reservationId == null || checkIn > x.checkout || checkOut < x.checkIn)
                                      select room).ToList();
                }

                foreach (room entityRoom in entityRoomList)
                {
                    Room room = new Room();
                    roomList.Add(room.ConvertTo(entityRoom));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.InnerException.Message);
            }

            return roomList;
        }

        enum RoomState
        {
            Vacant = 0,
            Occupied = 1,
            Dirty = 2,
            Reserved = 3,
        }

        enum ReservationState
        {
            Tentative = 0,
            Confirmed = 1,
            CheckedIn = 2,
            CheckedOut = 3
        }
    }
}
