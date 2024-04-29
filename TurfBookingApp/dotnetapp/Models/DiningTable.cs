using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models

{
    public class DiningTable
    {
        public int DiningTableID { get; set; }
        public int SeatingCapacity { get; set; }
        public bool Availability { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}