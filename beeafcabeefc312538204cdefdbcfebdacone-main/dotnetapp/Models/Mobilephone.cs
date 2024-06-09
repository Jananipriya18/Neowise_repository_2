using System;

namespace dotnetapp.Models
{
        public class MobilePhone
    {
        public int MobilePhoneId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}