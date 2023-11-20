using System.ComponentModel.DataAnnotations;

namespace Zencareservice.Models
{
    public class VerifyOtpViewModel
    {

        [Required(ErrorMessage = "Please enter the OTP.")]
        public string UserEnteredOtp { get; set; }

        public string GeneratedOtp { get; set; }
    }
}
