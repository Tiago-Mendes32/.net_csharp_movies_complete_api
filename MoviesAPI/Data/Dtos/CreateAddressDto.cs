﻿using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos;

public class CreateAddressDto
{
    public string Street { get; set; }
    public int Number { get; set; }
}
