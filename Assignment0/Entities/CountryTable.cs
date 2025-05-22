using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Assignment0.Entities
{

    [Index(nameof(CountryName), IsUnique=true)]
    public class CountryTable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string CountryName { get; set; }


    }
}
