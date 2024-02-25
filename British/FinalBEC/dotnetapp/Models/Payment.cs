﻿using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentID { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(EnquiryID))]
        public int? EnquiryID { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ModeOfPayment { get; set; }
        // public string TransactionID { get; set; }
        [ForeignKey(nameof(UserId))]
        public long UserId { get; set; }
        public int CourseID { get; set; }
        // public long? StudentId { get; set; }
        public User? Users { get; set; }
        public Course? Courses { get; set; }
        public Student? Students { get; set; }
        
    }
}
