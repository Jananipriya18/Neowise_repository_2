using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace dotnetapp.Models
{
public class Cart
    {
        [Key]
        public long CartId { get; set; }
        // public long GiftId { get; set; } // Foreign key
        public List<Gift>? Gifts { get; set; }
        // public List<Gift> Gifts { get; set; } = new List<Gift>();
        public long CustomerId { get; set; } // Foreign key
        public Customer? Customer { get; set; }
        public double TotalAmount { get; set; }
    }
}