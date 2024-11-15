using Microsoft.EntityFrameworkCore;
using MusicStreaming.Core.DTOs;
using MusicStreaming.Core.Interfaces;
using MusicStreaming.Core.Models;

namespace MusicStreaming.Core.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IRepository _repository;

    public PlaylistService(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<Playlist>> GetAllPlaylistsForUser(Guid userId)
    {
        return await _repository.GetAll<Playlist>()
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }
    
    public async Task<Playlist> CreatePlaylist(PlaylistDto playlistDto, Guid userId)
    {
        var playlist = new Playlist
        {
            Id = Guid.NewGuid(),
            Name = playlistDto.Name,
            Description = playlistDto.Description,
            UserId = userId
        };

        await _repository.AddAsync(playlist);
        await _repository.SaveChangesAsync();
        return playlist;
    }


    public async Task<Playlist?> GetPlaylistById(Guid id)
    {
        return await _repository.GetAll<Playlist>().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Playlist> UpdatePlaylist(Guid playlistId, PlaylistDto playlistDto, Guid userId)
    {
        var existingPlaylist = await _repository.GetAll<Playlist>()
            .FirstOrDefaultAsync(p => p.Id == playlistId);

        if (existingPlaylist == null)
            throw new Exception($"Playlist with ID {playlistId} not found.");
        
        if (existingPlaylist.UserId != userId)
            throw new UnauthorizedAccessException("You do not have permission to update this playlist.");
        
        existingPlaylist.Name = playlistDto.Name ?? existingPlaylist.Name;
        existingPlaylist.Description = playlistDto.Description ?? existingPlaylist.Description;
        
        _repository.Update(existingPlaylist);
        await _repository.SaveChangesAsync();

        return existingPlaylist;
    }

    public async Task DeletePlaylist(Guid id)
    {
        var playlist = await GetPlaylistById(id);
        if (playlist != null)
        {
            _repository.Delete(playlist);
            await _repository.SaveChangesAsync();
        }
    }
}