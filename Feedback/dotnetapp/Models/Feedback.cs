// FeedbackModel.cs

using System;
using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models{
public class Feedback
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter your name.")]
    [StringLength(100, ErrorMessage = "Name should not exceed 100 characters.")]
    public string StudentName { get; set; }

    [Required(ErrorMessage = "Please select the course.")]
    public string Course { get; set; }

    [Required(ErrorMessage = "Please provide feedback.")]
    [StringLength(500, ErrorMessage = "Feedback should not exceed 500 characters.")]
    public string Feedbacks { get; set; }

    [Range(1, 5, ErrorMessage = "Rating should be between 1 and 5.")]
    public int Rating { get; set; }

    [Display(Name = "Date Submitted")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DateSubmitted { get; set; } = DateTime.Now;
}
}