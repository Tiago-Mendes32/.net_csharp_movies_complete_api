using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;
using System.Diagnostics;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CinemaController : ControllerBase
{
    private MovieContext _context;
    private IMapper _mapper;

    public CinemaController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddCinema([FromBody]CreateCinemaDto cinemaDto)
    {
        Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
        _context.Add(cinema);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetCinemaById), new { id = cinema.Id }, cinema);
    }

    [HttpGet]
    public IEnumerable<ReadCinemaDto> GetCinemas([FromQuery] int skip = 0, [FromQuery] int take = 10) 
    {
        IEnumerable<Cinema> cinemas = _context.Cinemas;
        return _mapper.Map<IEnumerable<ReadCinemaDto>>(cinemas).Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetCinemaById(int id) 
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(c => c.Id == id);
        return cinema == null ? NotFound() : Ok(_mapper.Map<ReadCinemaDto>(cinema));
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCinema(int id, UpdateCinemaDto cinemaDto) 
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault<Cinema>(cinema => cinema.Id == id);
        if (cinema == null) {return NotFound();}

        _mapper.Map<Cinema>(cinemaDto);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateCinemaPatch(int id, [FromBody]JsonPatchDocument<UpdateCinemaDto> patch) 
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault<Cinema>(cinema => cinema.Id == id);
        if (cinema == null) { return NotFound(); }

        UpdateCinemaDto toUpdateCinema = _mapper.Map<UpdateCinemaDto>(cinema);
        patch.ApplyTo(toUpdateCinema, ModelState);

        if (!TryValidateModel(toUpdateCinema))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(toUpdateCinema, cinema);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCinema(int id) {
        Cinema cinema = _context.Cinemas.FirstOrDefault<Cinema>(cinema => cinema.Id == id);
        if (cinema == null) { return NotFound(); }

        _context.Remove(cinema);
        _context.SaveChanges();
        return NoContent();
    }



}
