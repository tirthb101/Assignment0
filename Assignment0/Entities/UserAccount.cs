using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Assignment0.Entities
{

    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }


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
       

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Education is required")]
        public string Education { get; set; }


        [Required(ErrorMessage = "Mobile Number is required")]
        [Length(10, 10, ErrorMessage = "Mobile number Must have Length equal to 10 charactors")]
        public string MobileNumber { get; set; }

        //[Required(ErrorMessage = "Profile Picture is required")]
        //public string ProfilePicturePath { get; set; }



    }
}
