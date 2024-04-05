using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class BeautySpa
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Service name is required")]
            public string Name { get; set; }

            [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
            public string Description { get; set; }

            [Required(ErrorMessage = "Price is required")]
            [Range(0, 10000, ErrorMessage = "Price must be between $0 and $10,000")]
            public decimal Price { get; set; }

            [Display(Name = "Duration (minutes)")]
            public int DurationInMinutes { get; set; }

            [Required(ErrorMessage = "Category is required")]
            public string Category { get; set; }
        }
    }