using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        [ForeignKey(nameof(FeedbackId))]
        public int UserId { get; set; }

        public string FeedbackText { get; set; }
        public DateTime Date { get; set; }

        public User? User { get; set; }
    }
}
