    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    namespace dotnetapp.Models

    {
        public class Turf
        {
            public int TurfID { get; set; }
            public string Name { get; set; }
            public int Capacity { get; set; }
            public bool Availability { get; set; }
            public List<Booking> Bookings { get; set; }
        }
    }