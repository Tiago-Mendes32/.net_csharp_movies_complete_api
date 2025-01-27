using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos;

public class CreateAddressDto
{
    [Required(ErrorMessage = "The street can't be null")]
    [MaxLength(120, ErrorMessage = "Max title length = 100")]
    public string Street { get; set; }
    [Required(ErrorMessage = "The number can't be null")]
    public int Number { get; set; }
}
