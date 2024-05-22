// using System;
// using System.ComponentModel.DataAnnotations;

// namespace dotnetapp.Models
// {
//     public class Recipe
//     {
//         [Key]
//         public int recipeId { get; set; }

//         [Required(ErrorMessage = "Recipe name is required")]
//         public string name { get; set; }

//         public string description { get; set; }

//         [Required(ErrorMessage = "Ingredients are required")]
//         public string ingredients { get; set; }

//         [Required(ErrorMessage = "Instructions are required")]
//         public string instructions { get; set; }

//         public string author { get; set; }

//     }
// }
using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Laptop
    {
        [Key]
        public int laptopId { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        public string brand { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string model { get; set; }

        public string description { get; set; }

        [Required(ErrorMessage = "Processor details are required")]
        public string processor { get; set; }

        [Required(ErrorMessage = "Storage details are required")]
        public string storage { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid price")]
        public decimal price { get; set; }

    }
}
