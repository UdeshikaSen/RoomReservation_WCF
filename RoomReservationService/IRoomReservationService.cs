using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RoomReservationService
{
    [ServiceContract]
    public interface IRoomReservationService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetUser/{userId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        User GetUser(string userId);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetUsers", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<User> GetUsers();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/RegisterUser", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string RegisterUser(string userEmail, string userName, string userPhone, string userAddress);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetRooms", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Room> GetRooms();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetRoomTypes", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<String> GetRoomTypes();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetAvailableRooms/{roomType}/{checkIn}/{checkOut}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Room> GetAvailableRooms(string roomType, string checkIn, string checkOut);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/ReserveRoom", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string ReserveRoom(string roomNo, int adults, int children, double reservationCost, string userEmail, DateTime inDateTime, DateTime outDateTime);

    }

    [DataContract]
    public class User
    {
        string userId;
        string name;
        string phone;
        string address;

        [DataMember]
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        [DataMember]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public User ConvertTo(reservation_user entityUser)
        {
            this.userId = entityUser.email;
            this.name = entityUser.name;
            this.phone = entityUser.phone;
            this.address = entityUser.user_address;
            return this;
        }
    }


    [DataContract]
    public class Reservation
    {
        string reservationId;
        DateTime reservationDate;
        int noOfAdults;
        int noOfChildren;
        double totalCost;
        string email;

        [DataMember]
        public string ReservationId
        {
            get { return reservationId; }
            set { reservationId = value; }
        }

        [DataMember]
        public DateTime ReservationDate
        {
            get { return reservationDate; }
            set { reservationDate = value; }
        }

        [DataMember]
        public int NoOfAdults
        {
            get { return noOfAdults; }
            set { noOfAdults = value; }
        }

        [DataMember]
        public int NoOfChildren
        {
            get { return noOfChildren; }
            set { noOfChildren = value; }
        }

        [DataMember]
        public double TotalCost
        {
            get { return totalCost; }
            set { totalCost = value; }
        }

        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }


        public Reservation ConvertTo(reservation entityReservation)
        {
            this.reservationId = entityReservation.reservationId;
            this.reservationDate = entityReservation.reservationDate.Value;
            this.noOfAdults = entityReservation.noOfAdults.Value;
            this.NoOfChildren = entityReservation.noOfChildren.Value;
            this.totalCost = entityReservation.totalCost.Value;
            this.email = entityReservation.email;
            return this;
        }
    }

    [DataContract]
    public class Room
    {
        string roomNo;
        string roomType;
        string price;
        string noOfBeds;
        string maxNoOfPeople;
        string roomState;

        [DataMember]
        public string RoomNo
        {
            get { return roomNo; }
            set { roomNo = value; }
        }

        [DataMember]
        public string RoomType
        {
            get { return roomType; }
            set { roomType = value; }
        }

        [DataMember]
        public string Price
        {
            get { return price; }
            set { price = value; }
        }

        [DataMember]
        public string NoOfBeds
        {
            get { return noOfBeds; }
            set { noOfBeds = value; }
        }

        [DataMember]
        public string MaxNoOfPeople
        {
            get { return maxNoOfPeople; }
            set { maxNoOfPeople = value; }
        }

        [DataMember]
        public string RoomState
        {
            get { return roomState; }
            set { roomState = value; }
        }

        public Room ConvertTo(room entityRoom)
        {
            this.roomNo = entityRoom.roomNo;
            this.roomType = entityRoom.roomType;
            this.price = entityRoom.price;
            this.noOfBeds = entityRoom.noOfBeds;
            this.maxNoOfPeople = entityRoom.maxNoOfPeople;
            this.roomState = entityRoom.roomState;
            return this;
        }
    }

    [DataContract]
    public class Room_reservation
    {
        DateTime checkIn;
        DateTime checkout;
        string reservationState;
        string roomNo;
        string reservationId;

        [DataMember]
        public DateTime CheckIn
        {
            get { return checkIn; }
            set { checkIn = value; }
        }

        [DataMember]
        public DateTime Checkout
        {
            get { return checkout; }
            set { checkout = value; }
        }

        [DataMember]
        public string ReservationState
        {
            get { return reservationState; }
            set { reservationState = value; }
        }

        [DataMember]
        public string RoomNo
        {
            get { return roomNo; }
            set { roomNo = value; }
        }

        [DataMember]
        public string ReservationId
        {
            get { return reservationId; }
            set { reservationId = value; }
        }

        public Room_reservation ConvertTo(room_reservation entityRoomReservation)
        {
            this.checkIn = entityRoomReservation.checkIn.Value;
            this.checkout = entityRoomReservation.checkout.Value;
            this.reservationState = entityRoomReservation.reservationState;
            this.roomNo = entityRoomReservation.roomNo;
            this.reservationId = entityRoomReservation.reservationId;
            return this;
        }
    }

}
