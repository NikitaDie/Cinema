using AutoMapper;
using Cinema.Core.DTOs;
using Cinema.Core.Helpers.UnifiedResponse;
using Cinema.Core.Interfaces;
using Cinema.Core.Interfaces.Extra;
using Cinema.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Core.Services;

public class MovieService : IMovieService
{
    private readonly IMapper _mapper;
    private readonly IRepository _repository;
    private readonly IFileUploadService _fileUploadService;

    public MovieService(IRepository repository, IMapper mapper, IFileUploadService fileUploadService)
    {
        _mapper = mapper;
        _repository = repository;
        _fileUploadService = fileUploadService;
    }
    
    public async Task<Result<MovieDetailsDto>> GetMovieDetails(Guid id)
    {
        var movie = await _repository.GetAll<Movie>()
            .Include(m => m.Genres)
            .Include(m => m.Starring)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (movie == null)
        {
            return Result.Failure<MovieDetailsDto>("Movie not found.");
        }
        
        var movieDetailsDto = _mapper.Map<MovieDetailsDto>(movie);
        
        movieDetailsDto.ImageUri = _fileUploadService.GetFileUrl(movie.ImagePath); 
        
        return Result.Success(movieDetailsDto);
    }

    public async Task<Result> DeleteMovie(Guid id)
    {
        var movie = _repository.GetAll<Movie>()
            .Include(m => m.Sessions)
            .FirstOrDefault(m => m.Id == id);
        
        if (movie == null)
            return Result.Failure("Movie not found.");
        
        movie.Sessions.ToList().ForEach(pl => _repository.Delete(pl));
        
        _repository.Delete(movie);
        await _repository.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result<ICollection<MovieMinimalDto>>> GetAllCurrentMovies()
    {
        // "current" means movies where rental period is still active
        var movies = _repository.GetAll<Movie>().AsNoTracking();
        var currentMovies = movies.Where(m => m.RentalPeriodEnd > DateOnly.FromDateTime(DateTime.UtcNow));
        
        var resultList = await currentMovies.ToListAsync();
        var mappedResultList = _mapper.Map<ICollection<MovieMinimalDto>>(resultList);
        
        foreach (var movie in mappedResultList) // Map image URI
        {
            movie.ImageUri = _fileUploadService.GetFileUrl(movie.ImageUri); 
        }
        
        return Result.Success(mappedResultList);
    }

    public async Task<Result<MovieMinimalDto>> CreateMovie(CreateMovieDto createMovieDto)
    {
        // Handle file upload
        var uploadResult = await UploadMovieImage(createMovieDto.Image);
        if (!uploadResult.IsSuccess)
            return Result.Failure<MovieMinimalDto>(uploadResult.Error);

        // Fetch related genres
        var genresResult = await FetchGenres(createMovieDto.Genres);
        if (!genresResult.IsSuccess)
            return Result.Failure<MovieMinimalDto>(genresResult.Error);

        // Fetch related actors
        var actorsResult = await FetchActors(createMovieDto.Starring);
        if (!actorsResult.IsSuccess)
            return Result.Failure<MovieMinimalDto>(actorsResult.Error);

        // Create movie entity
        var movie = _mapper.Map<Movie>(createMovieDto);
        movie.Genres = genresResult.Data ?? new List<Genre>();     // Assign retrieved genres
        movie.Starring = actorsResult.Data ?? new List<Actor>();   // Assign retrieved actors
        movie.ImagePath = _fileUploadService.GetFileUrl(uploadResult.Data!); // Assign uploaded image name

        // Save to repository
        await _repository.AddAsync(movie);
        await _repository.SaveChangesAsync();

        return Result.Success(_mapper.Map<MovieMinimalDto>(movie));
    }

    #region Helpers
    
    private async Task<Result<string>> UploadMovieImage(IFormFile image)
    {
        try
        {
            var fileName = await _fileUploadService.UploadFile(image);
            return Result.Success(fileName);
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