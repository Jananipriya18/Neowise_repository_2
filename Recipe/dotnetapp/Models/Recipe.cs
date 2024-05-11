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

        public string email { get; set; }

        [Required(ErrorMessage = "SubjectsOffered are required")]
        public string subjectsOffered { get; set; }

        [Required(ErrorMessage = "ContactNumber are required")]
        public string contactNumber { get; set; }

        public string author { get; set; }

    }
}
