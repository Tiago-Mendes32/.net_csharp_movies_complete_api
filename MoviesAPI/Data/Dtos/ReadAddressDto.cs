using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos;

public class ReadAddressDto
{
    public int Id { get; set; }
    public string Street { get; set; }
    public int Number { get; set; }
}
