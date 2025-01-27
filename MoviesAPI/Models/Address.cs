using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "The street can't be null")]
        [MaxLength(120, ErrorMessage = "Max title length = 100")]
        public string Street { get; set; }
        [Required(ErrorMessage = "The number can't be null")]
        public int Number { get; set; }
        public virtual Cinema cinema { get; set; }
    }
}
