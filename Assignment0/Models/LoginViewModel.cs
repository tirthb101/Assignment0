using System.ComponentModel.DataAnnotations;

namespace Assignment0.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50, ErrorMessage = "Maximum 50 characotrs Allowed")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characotrs Allowed")]
        public string Password { get; set; }
    }
}
