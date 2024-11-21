using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_Backend.Controllers;

[Route("api/movies")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    
    // GET: api/movies
    [HttpGet]
    public async Task<IActionResult> GetAllCurrentMovies()
    {
        return Ok(await _movieService.GetAllCurrentMovies());
    }
    
    // POST: api/movies
    [HttpPost]
    public async Task<IActionResult> CreateMovie([FromForm] CreateMovieDto createMovieDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _movieService.CreateMovie(createMovieDto);

        if (!result.IsSuccess)
        {
            return StatusCode((int)result.StatusCode, result.Message);
        }

        return CreatedAtAction(nameof(GetAllCurrentMovies), new { id = result.MovieId }, result);
    }
    
}