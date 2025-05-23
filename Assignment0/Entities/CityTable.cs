using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment0.Entities
{
    public class CityTable
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "City name is required")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "StateId is required")]
        public int StateId { get; set; }

        [ForeignKey("StateId")]
        public StateTable State { get; set; }
    }
}
