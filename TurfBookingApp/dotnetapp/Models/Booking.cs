using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int? TurfID { get; set; } 
        public Turf? Turf { get; set; }

        // public DateTime ReservationDate { get; set; }
        // public TimeSpan TimeSlot { get; set; }


        public int DurationInMinutes { get; set; }
        public int NumberOfPlayers { get; set; }
    }
}