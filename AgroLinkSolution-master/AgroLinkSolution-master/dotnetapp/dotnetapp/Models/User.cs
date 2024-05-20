using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is Required")]
        public string Role { get; set; }
        [Required(ErrorMessage = "MobileNumber is Required")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "ProfileImage is Required")]
        public string ProfileImage { get; set; }
    }
}
