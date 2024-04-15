using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "Recipe name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Ingredients are required")]
        public string Ingredients { get; set; }

        [Required(ErrorMessage = "Instructions are required")]
        public string Instructions { get; set; }

        public string Author { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
