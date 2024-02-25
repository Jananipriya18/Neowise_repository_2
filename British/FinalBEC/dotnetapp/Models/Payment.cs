// using System;
// using System.Text.Json.Serialization;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace dotnetapp.Models
// {
//     public class Payment
//     {
//         [Key]
//         public int PaymentID { get; set; }
//         [JsonIgnore]
//         [ForeignKey(nameof(EnquiryID))]
//         public int EnquiryID { get; set; }
//         public decimal AmountPaid { get; set; }
//         public DateTime PaymentDate { get; set; }
//         public string ModeOfPayment { get; set; }

//         [ForeignKey(nameof(UserId))]
//         public long UserId { get; set; }
//         public User? User { get; set; }

//         [ForeignKey(nameof(Course))]
//         public int CourseID { get; set; }
//         public Course? Course { get; set; }
        
//     }
// }


using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace dotnetapp.Models
{
public class Payment
{
    [Key]
    public int PaymentID { get; set; }
   
    public decimal AmountPaid { get; set; }
    // public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; }
    // public string TransactionID { get; set; }
    public long UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
    public int CourseID { get; set; }

    [ForeignKey(nameof(CourseID))]
    public Course? Course { get; set; }
}
}