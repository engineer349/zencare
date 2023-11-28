using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Zencareservice.Models
{
    public class Signup
    {

        
        public string Rcode { get; set; }

        [Required(ErrorMessage = "Firstname required!")]
        [DataType(DataType.Text)]
        [StringLength(13, MinimumLength = 4)]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname required!")]
        [DataType(DataType.Text)]
        [StringLength(13, MinimumLength = 4)]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Please enter Emailaddress")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [MaxLength(16)]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirm Password required")]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match.")]
        [MaxLength(16)]

        public string Confirmpassword { get; set; }

        [Required(ErrorMessage = "Enter Username")]
        [MaxLength(16)]

        public string Username { get; set; }


        [Required(ErrorMessage = "Phonenumber required")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Please enter a valid phone number.")]
        //[MaxLength(10)]
        public string Phonenumber { get; set; }

        [Required(ErrorMessage = "DOB required")]
        [DataType(DataType.Date)]

        public DateTime Dob { get; set; }

        public string Randomcode { get; set; }
        public int Status { get; set; }


        [Required(ErrorMessage = "Please select the role")]
        [DataType(DataType.Text)]
        public string Role { get; set; }

    }
}
