using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Assignment0.Models
{
   
    public class RegistrationViewModel
    {
  

                [Required(ErrorMessage = "Name is required")]
                [MaxLength(50, ErrorMessage = "Maximum 50 characotrs Allowed")]
                public string Name { get; set; }


                [Required(ErrorMessage = "Email is required")]
                [DataType(DataType.EmailAddress)]
                [MaxLength(50, ErrorMessage = "Maximum 50 characotrs Allowed")]
                public string Email { get; set; }


                [Required(ErrorMessage = "Password is required")]
                [DataType(DataType.Password)]
                [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
                [MaxLength(50, ErrorMessage = "Maximum 50 characotrs Allowed")]
                public string Password { get; set; }


                [DataType(DataType.Password)]
                [Compare("Password", ErrorMessage = "Ensure that your passwords match")]
                public string ConfirmPassword { get; set; }


                [Required(ErrorMessage = "Gender is required")]
                public string Gender { get; set; }


                [Required(ErrorMessage = "Education is required")]
                public string Education { get; set; }


                [Required(ErrorMessage = "Mobile Number is required")]
                [Length(10, 10, ErrorMessage = "Mobile number Must have Length equal to 10 charactors")]
                public string MobileNumber { get; set; }


                [Required(ErrorMessage = "Profile Picture is required")]
                [DataType(DataType.Upload)]
                public IFormFile ProfilePicture { get; set; }




    }
}
