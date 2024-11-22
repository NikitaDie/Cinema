using AutoMapper;
using Cinema.Core.DTOs;
using Cinema.Core.Helpers;
using Cinema.Core.Interfaces;
using Cinema.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cinema.Core.Services;

public class MovieService : IMovieService
{
    private readonly string _uploadsDirectory;
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public MovieService(IRepository repository, IConfiguration configuration, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
        _uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), configuration["UploadsDirectory"] ?? "public");
    }
    
    public Task<Result<MovieDetailsDto>> GetMovieDetails(int id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Result<ICollection<Movie>>> GetAllCurrentMovies()
    {
        // "current" means movies where rental period is still active
        var movies = _repository.GetAll<Movie>();
        var currentMovies = movies.Where(m => m.RentalPeriodEnd > DateTime.UtcNow);
        
        var resultList = await currentMovies.ToListAsync();
        return Result.Success((ICollection<Movie>)resultList);
    }

    public async Task<Result<Movie>> CreateMovie(CreateMovieDto createMovieDto)
    {
        // Validate DTO
        var validationResult = ValidateCreateMovieDto(createMovieDto);
        if (!validationResult.IsSuccess)
            return Result.Failure<Movie>(validationResult.Error);

        // Handle file upload
        var uploadResult = await UploadMovieImage(createMovieDto.Image);
        if (!uploadResult.IsSuccess)
            return Result.Failure<Movie>(uploadResult.Error);

        // Fetch related genres
        var genresResult = await FetchGenres(createMovieDto.Genres);
        if (!genresResult.IsSuccess)
            return Result.Failure<Movie>(genresResult.Error);

        // Fetch related actors
        var actorsResult = await FetchActors(createMovieDto.Starring);
        if (!actorsResult.IsSuccess)
            return Result.Failure<Movie>(actorsResult.Error);

        // Create movie entity
        var movie = _mapper.Map<Movie>(createMovieDto);
        movie.Genres = genresResult.Data ?? new List<Genre>();     // Assign retrieved genres
        movie.Starring = actorsResult.Data ?? new List<Actor>();   // Assign retrieved actors
        
        try {
            movie.ImagePath = uploadResult.Data ?? throw new InvalidOperationException();   // Set uploaded file path
        } catch (Exception e) {
            Result.Failure<Movie>("Image upload failed");
        }

        // Save to repository
        await _repository.AddAsync(movie);
        await _repository.SaveChangesAsync();

        return Result.Success(movie);
    }

    #region Helpers

    private Result ValidateCreateMovieDto(CreateMovieDto dto)
    {
        var errors = dto.Validate();
        return errors.Count > 0
            ? Result.Failure(string.Join(",", errors))
            : Result.Success();
    }
    
    private async Task<Result<string>> UploadMovieImage(IFormFile image)
    {
        try
        {
            var uploadDirectory = Path.Combine(_uploadsDirectory, "movies");
            var filePath = await FileUploadService.UploadFile(image, uploadDirectory);
            return Result.Success(filePath);
        }
        catch (Exception e)
        {
            return Result.Failure<string>($"Image upload failed: {e.Message}");
        }
    }
    
    private async Task<Result<List<Genre>>> FetchGenres(ICollection<Guid> genreIds)
    {
        var genres = await _repository.GetAll<Genre>()
            .Where(g => genreIds.Contains(g.Id))
            .ToListAsync();

        if (genres.Count != genreIds.Count)
            return Result.Failure<List<Genre>>("Some genres were not found.");

        return Result.Success(genres);
    }

    private async Task<Result<List<Actor>>> FetchActors(ICollection<Guid>? actorIds)
    {
        if (actorIds == null || actorIds.Count == 0)
            return Result.Success(new List<Actor>());

        var actors = await _repository.GetAll<Actor>()
            .Where(a => actorIds.Contains(a.Id))
            .ToListAsync();

        if (actors.Count != actorIds.Count)
            return Result.Failure<List<Actor>>("Some actors were not found.");

        return Result.Success(actors);
    }

    #endregion
    
}