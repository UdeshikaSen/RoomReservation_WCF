﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RoomReservationEntities : DbContext
    {
        public RoomReservationEntities()
            : base("name=RoomReservationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<reservation> reservations { get; set; }
        public virtual DbSet<reservation_user> reservation_users { get; set; }
        public virtual DbSet<room> rooms { get; set; }
        public virtual DbSet<room_reservation> room_reservations { get; set; }
    }
}
