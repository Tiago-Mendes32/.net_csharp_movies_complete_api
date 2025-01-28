using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos;

public class ReadMovieDto
{
    public string Title { get; set; }
    public string Gender { get; set; }
    public int Duration { get; set; }
    public DateTime ConsultDateTime { get; set; } = DateTime.Now;
    public ICollection<ReadSessionDto> Sessions { get; set; }

}
