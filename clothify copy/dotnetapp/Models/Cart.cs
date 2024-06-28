using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace dotnetapp.Models
{
public class Cart
    {
        [Key]
        public long CartId { get; set; }
        // public long ProductId { get; set; } // Foreign key
        public List<Product>? Products { get; set; }
        // public List<Product> Products { get; set; } = new List<Product>();
        public long CustomerId { get; set; } // Foreign key
        public Customer? Customer { get; set; }
        public double TotalAmount { get; set; }
    }
}