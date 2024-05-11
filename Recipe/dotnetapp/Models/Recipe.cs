using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Recipe
    {
        [Key]
        public int tutorId { get; set; }

        [Required(ErrorMessage = "Recipe name is required")]
        public string name { get; set; }

        public string description { get; set; }

        [Required(ErrorMessage = "Ingredients are required")]
        public string ingredients { get; set; }

        [Required(ErrorMessage = "Instructions are required")]
        public string instructions { get; set; }

        public string author { get; set; }

    }
}
