using System;
namespace dotnetapp.Models
{
    public class Member
    {
        public int MemberId {get; set;}
        public string Firstname {get; set;}
        public string LastName {get; set;}
        public DateTime DateOfBirth {get; set;}
        public string Address{get; set;}
        public string Email {get; set;}
        public DateTime RegistrationDate {get; set;}

    }
}
