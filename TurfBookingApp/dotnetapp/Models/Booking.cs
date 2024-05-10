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
        public string CustomerName { get; set; }
        public string ContactNumber { get; set; }
        public int DurationInMinutes { get; set; }

    }
}