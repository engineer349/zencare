using System.ComponentModel.DataAnnotations;

namespace Zencareservice.Models
{
    public class Login
    {

        public int LoginId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
