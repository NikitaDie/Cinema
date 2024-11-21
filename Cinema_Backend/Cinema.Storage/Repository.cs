using Cinema.Core.Interfaces;
using Cinema.Storage.Contexts;

namespace Cinema.Storage;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> GetAll<T>() where T : class
    {
        return _context.Set<T>();
    }
    
    public async Task AddAsync<T>(T entity) where T : class
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}