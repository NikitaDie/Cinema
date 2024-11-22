using Cinema.Core.DTOs;
using Cinema.Core.Helpers;
using Cinema.Core.Models;

namespace Cinema.Core.Interfaces;

public interface IMovieService
{
    public Task<Result<ICollection<Movie>>> GetAllCurrentMovies();
    
    Task<Result<Movie>> CreateMovie(CreateMovieDto createMovieDto);
    Task<Result<MovieDetailsDto>> GetMovieDetails(int id);
}