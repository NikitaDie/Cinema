using Microsoft.EntityFrameworkCore;
using MusicStreaming.Core.DTOs;
using MusicStreaming.Core.Interfaces;
using MusicStreaming.Core.Models;

namespace MusicStreaming.Core.Services;

public class SongService : ISongService
{
    private readonly IRepository _repository;

    public SongService(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Song> CreateSong(SongDto songDto, Guid userId)
    {
        var song = new Song
        {
            Title = songDto.Title,
            Artist = songDto.Artist,
            Album = songDto.Album,
            Duration = songDto.Duration,
            Genre = songDto.Genre,
            Url = songDto.Url
        };

        await _repository.AddAsync(song);
        await _repository.SaveChangesAsync();

        return song;
    }

    public async Task<Song?> GetSongById(Guid id)
    {
        return await _repository.GetAll<Song>()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public Task DeleteSong(Guid songId, Guid userId)
    {
        throw new NotImplementedException();
    }
    
    public async Task AddSongToPlaylist(Guid songId, Guid playlistId, Guid userId)
    {
        var song = await _repository.GetAll<Song>()
            .FirstOrDefaultAsync(s => s.Id == songId);

        if (song == null)
            throw new KeyNotFoundException($"Song with ID {songId} not found.");

        var playlist = await _repository.GetAll<Playlist>()
            .FirstOrDefaultAsync(p => p.Id == playlistId);

        if (playlist == null)
            throw new KeyNotFoundException($"Playlist with ID {playlistId} not found.");

        if (playlist.UserId != userId)
            throw new UnauthorizedAccessException("You do not have permission to add a song to this playlist.");
        
        playlist.Songs.Add(song);
        await _repository.SaveChangesAsync();
    }

    public async Task RemoveSongFromPlaylist(Guid songId, Guid playlistId, Guid userId)
    {
        var song = await _repository.GetAll<Song>()
            .FirstOrDefaultAsync(s => s.Id == songId);

        if (song == null)
            throw new KeyNotFoundException($"Song with ID {songId} not found.");

        var playlist = await _repository.GetAll<Playlist>()
            .FirstOrDefaultAsync(p => p.Id == playlistId);

        if (playlist == null)
            throw new KeyNotFoundException($"Playlist with ID {playlistId} not found.");

        if (playlist.UserId != userId)
            throw new UnauthorizedAccessException("You do not have permission to remove a song from this playlist.");
        
        playlist.Songs.Remove(song);
        await _repository.SaveChangesAsync();
    }
}