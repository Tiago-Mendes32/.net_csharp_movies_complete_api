using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos;

public class ReadSessionDto
{
    public int MovieId { get; set; }
    public int CinemaId { get; set; }
}
