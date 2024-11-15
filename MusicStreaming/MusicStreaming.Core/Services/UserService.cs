using Microsoft.EntityFrameworkCore;
using MusicStreaming.Core.Interfaces;
using MusicStreaming.Core.Models;

namespace MusicStreaming.Core.Services;

public class UserService : IUserService
{
    private readonly IRepository _repository;
    
    public UserService(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<User?> GetUserById(Guid id)
    {
        return await _repository.GetAll<User>()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> UserExists(string email)
    {
        return await _repository.GetAll<User>()
            .AnyAsync(u => u.Email == email);
    }

    public async Task<User> CreateUser(User user)
    {
        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _repository.GetAll<User>()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task UpdateUser(User user)
    {
        var existingUser = await _repository.GetAll<User>()
            .FirstOrDefaultAsync(u => u.Id == user.Id);

        if (existingUser != null)
        {
            _repository.Update(existingUser);
            await _repository.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("User not found.");
        }
    }
    public async Task DeleteUser(Guid id)
    {
        var user = await _repository.GetAll<User>()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user != null)
        {
            _repository.Delete(user);
            await _repository.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("User not found.");
        }
    }
}