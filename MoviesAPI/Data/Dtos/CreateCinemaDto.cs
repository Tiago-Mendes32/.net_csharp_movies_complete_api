using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos;

public class CreateCinemaDto
{
    [Required(ErrorMessage = "The name can't be null")]
    [StringLength(100, ErrorMessage = "Max title length = 100")]
    public string Name { get; set; }
}
