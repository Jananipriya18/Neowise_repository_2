using System;
using System.ComponentModel.DataAnnotations;
 
namespace dotnetapp.Models
{
public class Order
{
    public long OrderId { get; set; }
    public double OrderPrice { get; set; }
    public int? Quantity { get; set; }
   
    // One-to-Many relationship with Product
    public List<Product> Products { get; set; }
    public long? CustomerId { get; set; }
    // Many-to-One relationship with Customer
    public Customer? Customer { get; set; }
}
}