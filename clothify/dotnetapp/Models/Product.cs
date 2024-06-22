using System;
using System.ComponentModel.DataAnnotations;
 
namespace dotnetapp.Models
{
public class Product
    {
        [Key]
        public long ProductId { get; set; }
        public string ProductType { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductDetails { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public long? CartId { get; set; } // Foreign key
        public Cart? Cart { get; set; }
        public long? OrderId { get; set; } // Foreign key
        public Order? Order { get; set; }
    }
}