using MusicStreaming.Core.Models;

namespace MusicStreaming.Core.Interfaces;

public interface IUserService
{
    Task<User?> GetUserById(Guid id);
    Task<bool> UserExists(string email);
    Task<User> CreateUser(User user);
    Task<User?> GetUserByEmail(string email);
    Task UpdateUser(User user);
    Task DeleteUser(Guid id);
}