using Assignment0.Entities;
using System.ComponentModel.DataAnnotations;

public class StateTable
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "State is required")]
    public string StateName { get; set; }

    [Required(ErrorMessage = "CountryId is required")]
    public int CountryId { get; set; }
    public CountryTable Country { get; set; }
}
