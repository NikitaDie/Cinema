using Cinema.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_Backend.Controllers;

[Route("api/auditoriums")]
[ApiController]
public class AuditoriumController : ControllerBase
{
    private readonly IMovieService _movieService;

    public AuditoriumController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    
    
    
}