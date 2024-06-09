using System;
using System.Collections.Generic;

namespace dotnetapp.Models
{
    public partial class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime MembershipDate { get; set; }
    }
}
