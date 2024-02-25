using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
    public class Payment
    {
       
        // public int PaymentID { get; set; }
       
        // public decimal AmountPaid { get; set; }
        // public DateTime PaymentDate { get; set; }
        // public string ModeOfPayment { get; set; }
        // public long UserId { get; set; }
        // public int CourseID { get; set; }
        // public long? StudentId { get; set; }
        // public User? Users { get; set; }
        // public Course? Courses { get; set; }
        // public Student? Students { get; set; }
         [Key]
        public int PaymentID {get; set;}

        [ForeignKey(nameof(EnquiryID))]
        public int EnquiryID { get; set; }

        [ForeignKey(nameof(CourseID))]
        public Course? Course { get; set; }

        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        
        public int AmountPaid {get; set;}
        public DateTime PaymentDate {get; set;}
        public string ModeOfPayment {get; set;}
        public Student Student { get; set; }
    }
}
