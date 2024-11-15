using MusicStreaming.Core.DTOs;
using MusicStreaming.Core.Models;

namespace MusicStreaming.Core.Interfaces;

public interface IPlaylistService
{
    Task<List<Playlist>> GetAllPlaylistsForUser(Guid userId);
    Task<Playlist> CreatePlaylist(PlaylistDto playlistDto, Guid userId);
    Task<Playlist?> GetPlaylistById(Guid id);
    Task<Playlist> UpdatePlaylist(Guid playlistId, PlaylistDto playlistDto, Guid userId);
    Task DeletePlaylist(Guid id);
}