using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class GroceryItem
    {
        public int ItemId { get; set; }

        [Required]
        public string ItemName { get; set; }

        public string ItemDescription { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int QuantityAvailable { get; set; }

        [Required]
        public string Category { get; set; }
    }
}

