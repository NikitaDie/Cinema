using MusicStreaming.Core.DTOs;
using MusicStreaming.Core.Models;

namespace MusicStreaming.Core.Interfaces;

public interface ISongService
{
    Task<Song> CreateSong(SongDto songDto, Guid userId);
    Task<Song?> GetSongById(Guid id);
    Task DeleteSong(Guid songId, Guid userId);
    Task AddSongToPlaylist(Guid songId, Guid playlistId, Guid userId);
    Task RemoveSongFromPlaylist(Guid songId, Guid playlistId, Guid userId);
}