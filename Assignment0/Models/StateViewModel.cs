using System.ComponentModel.DataAnnotations;

namespace Assignment0.Models

{
    public class StateViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string StateName { get; set; }

        [Required(ErrorMessage = "CountryId is required")]
        public int CountryId { get; set; }
    }
}
