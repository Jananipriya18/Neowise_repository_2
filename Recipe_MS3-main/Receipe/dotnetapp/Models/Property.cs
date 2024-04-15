using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "Property name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip code is required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid zip code")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Property type is required")]
        public string PropertyType { get; set; }

        [Required(ErrorMessage = "Number of bedrooms is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        public int Bedrooms { get; set; }

        [Required(ErrorMessage = "Number of bathrooms is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        public int Bathrooms { get; set; }

        [Required(ErrorMessage = "Monthly rent amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid amount")]
        public double MonthlyRent { get; set; }

        public bool Available { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
