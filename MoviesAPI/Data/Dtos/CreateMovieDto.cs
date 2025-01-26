using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos;

public class CreateMovieDto
{
    [Required(ErrorMessage = "The title can't be null")]
    [StringLength(100, ErrorMessage = "Max title length = 100")]
    public string Title { get; set; }

    [Required(ErrorMessage = "The gender can't be null")]
    [StringLength(20, ErrorMessage = "Max gender length = 20")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "The duration can't be null")]
    [Range(70, 600, ErrorMessage = "The duration needs to be beetwen 70 and 600 minutes")]
    public int Duration { get; set; }
}
