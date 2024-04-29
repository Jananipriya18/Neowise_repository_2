using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int? TurfID { get; set; } // Change to nullable as it's no longer directly linked to turfs
        public Turf? Turf { get; set; }

        public DateTime ReservationDate { get; set; }
        public TimeSpan TimeSlot { get; set; }
    }
}