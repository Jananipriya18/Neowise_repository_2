using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models

{
    public class Booking
    {
        public int BookingID { get; set; }
        public int DiningTableID { get; set; }
        public DiningTable? DiningTable { get; set; }

        public DateTime ReservationDate { get; set; }
        public TimeSpan TimeSlot { get; set; }
    }
}