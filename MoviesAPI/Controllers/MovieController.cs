using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private MovieContext _context;
    private IMapper _mapper;

    public MovieController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adds a new movie to the database.
    /// </summary>
    /// <param name="movieDto">An object containing the required fields to create a new movie.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
    /// <response code="201">Returned if the movie was successfully created.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddMovie(
        [FromBody] CreateMovieDto movieDto)
    {
        Movie movie = _mapper.Map<Movie>(movieDto);
        _context.Add(movie);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
    }

    /// <summary>
    /// Retrieves a list of movies from the database.
    /// </summary>
    /// <param name="skip">The number of movies to skip for pagination.</param>
    /// <param name="take">The number of movies to take for pagination.</param>
    /// <returns>A collection of <see cref="ReadMovieDto"/> objects.</returns>
    /// <response code="200">Returned if the movies were successfully retrieved.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadMovieDto> GetMovies([FromQuery] int skip = 0, [FromQuery] int take = 5)
    {
        return _mapper.Map<List<ReadMovieDto>>(_context.Movies.Skip(skip).Take(take));
    }

    /// <summary>
    /// Retrieves a specific movie by its ID.
    /// </summary>
    /// <param name="id">The ID of the movie to retrieve.</param>
    /// <returns>An <see cref="IActionResult"/> containing the movie details.</returns>
    /// <response code="200">Returned if the movie was found.</response>
    /// <response code="404">Returned if the movie was not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetMovieById(int id)
    {
        Movie movie = _context.Movies
            .FirstOrDefault(movie => movie.Id == id);
        ReadMovieDto movieDto = _mapper.Map<ReadMovieDto>(movie);
        return movieDto != null ? Ok(movieDto) : NotFound();
    }

    /// <summary>
    /// Updates an existing movie in the database.
    /// </summary>
    /// <param name="id">The ID of the movie to update.</param>
    /// <param name="movieDto">An object containing the updated fields for the movie.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
    /// <response code="204">Returned if the movie was successfully updated.</response>
    /// <response code="404">Returned if the movie was not found.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDto movieDto)
    {
        Movie movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null)
        {
            return NotFound();
        }
        else
        {
            _mapper.Map(movieDto, movie);
            _context.SaveChanges();
            return NoContent();
        }

    }

    /// <summary>
    /// Partially updates an existing movie in the database.
    /// </summary>
    /// <param name="id">The ID of the movie to update.</param>
    /// <param name="patch">A JSON Patch document containing the fields to update.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
    /// <response code="204">Returned if the movie was successfully updated.</response>
    /// <response code="404">Returned if the movie was not found.</response>
    /// <response code="400">Returned if the patch operation was invalid.</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateMoviePatch(int id, [FromBody]JsonPatchDocument<UpdateMovieDto> patch)
    {
        Movie movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        UpdateMovieDto toUpdateMovie = _mapper.Map<UpdateMovieDto>(movie);

        patch.ApplyTo(toUpdateMovie, ModelState);

        if (!TryValidateModel(toUpdateMovie))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(toUpdateMovie, movie);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deletes a movie from the database.
    /// </summary>
    /// <param name="id">The ID of the movie to delete.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
    /// <response code="204">Returned if the movie was successfully deleted.</response>
    /// <response code="404">Returned if the movie was not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteMovie(int id)
    {
        Movie movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null)
        {
            return NotFound();
        }
        _context.Remove(movie);
        _context.SaveChanges();
        return NoContent();
    }
}
