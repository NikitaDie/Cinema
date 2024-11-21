using Cinema.Core.DTOs;
using Cinema.Core.Helpers;
using Cinema.Core.Models;

namespace Cinema.Core.Interfaces;

public interface IMovieService
{
    public Task<IEnumerable<Movie>> GetAllCurrentMovies();
    
    Task<ServiceResult> CreateMovie(CreateMovieDto createMovieDto);
}