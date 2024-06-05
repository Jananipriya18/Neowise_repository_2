using System;

namespace dotnetapp.Models
{
    public class InventoryItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }

}

