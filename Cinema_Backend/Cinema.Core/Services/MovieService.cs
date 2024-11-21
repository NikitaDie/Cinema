using Cinema.Core.DTOs;
using Cinema.Core.Helpers;
using Cinema.Core.Interfaces;
using Cinema.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Cinema.Core.Services;

public class MovieService : IMovieService
{
    private readonly IRepository _repository;

    public MovieService(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<Movie>> GetAllCurrentMovies()
    {
        // // Assuming "current" means movies where rental period is still active
        // var movies = _repository.GetAll<Movie>();
        // var currentMovies = movies.Where(m => m.RentalPeriodEnd > DateTime.UtcNow);
        // return currentMovies.ToArrayAsync();
        return null;
    }

    public async Task<ServiceResult> CreateMovie(CreateMovieDto createMovieDto)
    {
        var validationResult = createMovieDto.Validate();
        
        if (!validationResult.IsSuccess)
            return validationResult;

        string filePath;
        
        try
        {
            var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "public", "movies");

            // Upload the file
            filePath = await FileUploadService.UploadFile(createMovieDto.Image, uploadDirectory);
        }
        catch (Exception e)
        {
            return new ServiceResult(false, e.Message);
        }
        
        var movie = new Movie
        {
            Title = createMovieDto.Title,
            AgeRating = createMovieDto.AgeRating,
            ReleaseYear = createMovieDto.ReleaseYear,
            Language = createMovieDto.Language,
            Genres = string.Join(",", createMovieDto.Genres), // Store genres as comma-separated string
            Director = createMovieDto.Director,
            Duration = createMovieDto.Duration,
            RentalPeriodStart = createMovieDto.RentalPeriodStart,
            RentalPeriodEnd = createMovieDto.RentalPeriodEnd,
            TrailerLink = createMovieDto.TrailerLink,
            ImagePath = filePath,
        };
        
        await _repository.AddAsync(movie);
        await _repository.SaveChangesAsync();
        return new ServiceResult(true, "Movie added successfully!");
    }
    
}