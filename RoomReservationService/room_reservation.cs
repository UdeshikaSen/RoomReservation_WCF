//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoomReservationService
{
    using System;
    using System.Collections.Generic;
    
    public partial class room_reservation
    {
        public Nullable<System.DateTime> checkIn { get; set; }
        public Nullable<System.DateTime> checkout { get; set; }
        public string reservationState { get; set; }
        public string roomNo { get; set; }
        public string reservationId { get; set; }
    
        public virtual reservation reservation { get; set; }
        public virtual room room { get; set; }
    }
}