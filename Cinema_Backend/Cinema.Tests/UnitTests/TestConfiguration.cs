using AutoMapper;
using Cinema.Core;
using Cinema.Storage;
using Cinema.Storage.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Tests.UnitTests;

public class TestConfiguration : IDisposable
{
    protected readonly AppDbContext DbContext;
    protected readonly Repository Repository;
    protected readonly IMapper Mapper;
    
    public TestConfiguration()
    {
        // Setup in-memory database
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        DbContext = new AppDbContext(options);
        Repository = new Repository(DbContext);

        // Configure AutoMapper
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        Mapper = mapperConfig.CreateMapper();
    }
    
    public void Dispose()
    {
        DbContext.Database.EnsureDeleted(); // Clean up database after each test
        DbContext.Dispose();
    }
}