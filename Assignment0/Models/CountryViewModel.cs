using System.ComponentModel.DataAnnotations;

namespace Assignment0.Models
{
    public class CountryViewModel
    {
        [Required(ErrorMessage = "Country is required")]
        [MaxLength(15, ErrorMessage = "Maximum Length of country is 15")]
        public string CountryName { get; set; }
    }
}
