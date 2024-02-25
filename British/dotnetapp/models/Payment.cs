using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace dotnetapp.Models
{
public class Payment
{
    [Key]
    public class Payment
        {
        public int PaymentID {get; set;}
         [ForeignKey(nameof(EnquiryID))]
        public int EnquiryID { get; set; }
         [ForeignKey(nameof(CourseID))]
        public Course? Course { get; set; }
        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public int amountPaid {get; set;}
        public DateTime paymentDate {get; set;}
        public string modeOfPayment {get; set;}
        public Student Student { get; set; }
        }

}
}