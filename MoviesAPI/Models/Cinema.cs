using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models;

public class Cinema
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "The name can't be null")]
    [MaxLength(100, ErrorMessage = "Max title length = 100")]
    public string Name { get; set; }

    public int AddressId { get; set; }
    public virtual Address Address { get; set; }
}
