using System.ComponentModel.DataAnnotations;

namespace Assignment0.Models
{
    public class CityViewModel
    {
        [Required(ErrorMessage = "City name is required")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "StateId is required")]
        public int StateId { get; set; }
    }
}
