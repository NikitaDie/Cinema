using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
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
    
    //Get api/movies/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMovieDetails(Guid id)
    {
        var result = await _movieService.GetMovieDetails(id);
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
    
    // GET: api/movies
    [HttpGet]
    public async Task<IActionResult> GetAllCurrentMovies()
    {
        var result = await _movieService.GetAllCurrentMovies();
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
    
    // POST: api/movies
    [HttpPost]
    public async Task<IActionResult> CreateMovie([FromForm] CreateMovieDto createMovieDto)
    {
        var result = await _movieService.CreateMovie(createMovieDto);
        var createdMovieId = result.Data?.Id;
        
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetMovieDetails), new { id = createdMovieId }, result)
            : BadRequest(result.Error);
    }
    
}