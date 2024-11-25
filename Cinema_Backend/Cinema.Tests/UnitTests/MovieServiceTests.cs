using AutoMapper;
using Cinema.Core;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using Cinema.Core.Interfaces.Extra;
using Cinema.Core.Models;
using Cinema.Core.Services;
using Cinema.Storage;
using Cinema.Storage.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Cinema.Tests.UnitTests;

public class MovieServiceTests
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly Mock<IFileUploadService> _mockFileUploadService;
    private readonly MovieService _movieService;

    public MovieServiceTests()
    {
        // Create the AutoMapper configuration and get the mapper instance
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configuration.CreateMapper();
        
        // Set up EF Core InMemory Database
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database for isolation
            .Options;

        var dbContext = new AppDbContext(options);

        // Create repository using the in-memory database
        _repository = new Repository(dbContext);
        
        _mockConfiguration = new Mock<IConfiguration>();
        _mockFileUploadService = new Mock<IFileUploadService>();

        _mockConfiguration.Setup(c => c["UploadsDirectory"]).Returns("uploads");

       
        _movieService = new MovieService(
            _repository,
            _mockConfiguration.Object,
            _mapper,
            _mockFileUploadService.Object
        );
    }
  
    // [Fact]
    // public async Task ValidationFails_ReturnsFailure()
    // {
    //     // Arrange
    //     var invalidDto = new CreateMovieDto(); // Invalid DTO missing required fields
    //     
    //     // Act
    //     var result = await _movieService.CreateMovie(invalidDto);
    //     
    //     // Assert
    //     Assert.False(result.IsSuccess);
    //     Assert.Contains("Movie name is required", result.Error);
    // }

    [Fact]
    public async Task FileUploadFails_ReturnsFailure()
    {
        // Arrange
        var createMovieDto = new CreateMovieDto
        {
            Title = "Test Movie",
            AgeRating = 13,
            ReleaseYear = 2023,
            RentalPeriodStart = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            RentalPeriodEnd = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
            Language = "English",
            Genres = new List<Guid>(),
            Duration = new TimeSpan(2, 0, 0),
            TrailerLink = "https://youtube.com/test-trailer",
            Image = Mock.Of<IFormFile>()
        };

        _mockFileUploadService
            .Setup(s => s.UploadFile(It.IsAny<IFormFile>(), It.IsAny<string>()))
            .ThrowsAsync(new Exception("Upload failed"));

        // Act
        var result = await _movieService.CreateMovie(createMovieDto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Image upload failed", result.Error);
    }

    [Fact]
    public async Task GenresNotFound_ReturnsFailure()
    {
        // Arrange
        var genreId = Guid.NewGuid();
        var createMovieDto = new CreateMovieDto
        {
            Title = "Test Movie",
            AgeRating = 13,
            ReleaseYear = 2023,
            RentalPeriodStart = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            RentalPeriodEnd = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
            Language = "English",
            Genres = new List<Guid> { genreId }, // Genre ID that doesn't exist in the DB
            Duration = new TimeSpan(2, 0, 0),
            TrailerLink = "https://youtube.com/test-trailer",
            Image = Mock.Of<IFormFile>()
        };

        // Act
        var result = await _movieService.CreateMovie(createMovieDto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Some genres were not found", result.Error);
    }

    [Fact]
    public async Task AllValidInputs_ReturnsSuccess()
    {
        // Arrange
        var genreId = Guid.NewGuid();
        var actorId = Guid.NewGuid();

        // Seed data in the in-memory database
        await _repository.AddAsync(new Genre { Id = genreId, Name = "Action" });
        await _repository.AddAsync(new Actor { Id = actorId, FirstName = "Actor", LastName = "1"});
        await _repository.SaveChangesAsync();

        var createMovieDto = new CreateMovieDto
        {
            Title = "Test Movie",
            AgeRating = 13,
            ReleaseYear = 2023,
            Director = "Test Director",
            RentalPeriodStart = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            RentalPeriodEnd = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
            Language = "English",
            Genres = new List<Guid> { genreId },
            Starring = new List<Guid> { actorId },
            Duration = new TimeSpan(2, 0, 0),
            TrailerLink = "https://youtube.com/test-trailer",
            Image = Mock.Of<IFormFile>()
        };

        // Mock file upload
        _mockFileUploadService.Setup(f => f.UploadFile(It.IsAny<IFormFile>(), It.IsAny<string>()))
            .ReturnsAsync("test/path/image.jpg");

        var movie = _mapper.Map<Movie>(createMovieDto);

        // Act
        var result = await _movieService.CreateMovie(createMovieDto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Test Movie", result.Data?.Title);
        Assert.Equal("http://localhost:5054/uploads/ath/image.jpg", result.Data?.ImageUri);
    }
}