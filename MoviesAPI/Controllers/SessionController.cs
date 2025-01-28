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
public class SessionController : ControllerBase
{
    private MovieContext _context;
    private IMapper _mapper;

    public SessionController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddSession([FromBody] CreateSessionDto sessionDto)
    {
        Session session = _mapper.Map<Session>(sessionDto);
        _context.Sessions.Add(session);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetSessionById),new { MovieId = session.MovieId, CinemaId = session.CinemaId}, session);
    }

    [HttpGet]
    public IEnumerable<ReadSessionDto> GetSessions([FromQuery]int skip = 0, [FromQuery]int take = 10)
    {
        IEnumerable<Session> sessions = _context.Sessions.ToList().Skip(skip).Take(take);
        return _mapper.Map<IEnumerable<ReadSessionDto>>(sessions);
    }

    [HttpGet("{movieId}/{cinemaId}")]
    public IActionResult GetSessionById(int cinemaId, int movieId)
    {
        Session session = _context.Sessions.FirstOrDefault(s => s.MovieId == movieId &&  s.CinemaId == cinemaId);
        if (session == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ReadSessionDto>(session));
    }

    //[HttpPut("{id}")]
    //public IActionResult UpdateSession(int id, [FromBody] UpdateSessionDto updateSessionDto)
    //{
    //    Session session = _context.Sessions.FirstOrDefault(s => s.Id == id);
    //    if (session == null)
    //    {
    //        return NotFound();
    //    }
    //    _mapper.Map(updateSessionDto, session);
    //    _context.SaveChanges();
    //    return NoContent();
    //}

    //[HttpPatch("{id}")]
    //public IActionResult UpdateSessionPatch(int id, [FromBody]  JsonPatchDocument<UpdateSessionDto> patch)
    //{
    //    Session session = _context.Sessions.FirstOrDefault(s => s.Id == id);
    //    if (session == null)
    //    {
    //        return NotFound();
    //    }
    //    UpdateSessionDto toUpdateSession = _mapper.Map<UpdateSessionDto>(session);
    //    patch.ApplyTo(toUpdateSession, ModelState);
    //    if (!TryValidateModel(toUpdateSession))
    //    {
    //        return ValidationProblem(ModelState);
    //    }
    //    _mapper.Map(toUpdateSession, session);
    //    _context.SaveChanges();
    //    return NoContent();
    //}

    //[HttpDelete("{id}")]
    //public IActionResult DeleteSession(int id)
    //{
    //    Session session = _context.Sessions.FirstOrDefault(s => s.Id == id);
    //    if (session == null)
    //    {
    //        return NotFound();
    //    }
    //    _context.Sessions.Remove(session);
    //    _context.SaveChanges();
    //    return NoContent();
    //}
}
