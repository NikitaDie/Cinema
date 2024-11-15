using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStreaming.Core.DTOs;
using MusicStreaming.Core.Interfaces;

namespace MusicStreaming.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PlaylistController : ControllerBase
{
    private readonly IPlaylistService _playlistService;

    public PlaylistController(IPlaylistService playlistService)
    {
        _playlistService = playlistService;
    }

    // GET: api/Playlist
    [HttpGet]
    public async Task<IActionResult> GetAllPlaylists()
    {
        var userId = GetUserIdFromClaims(User);
    
        if (!userId.HasValue)
            return Unauthorized("User ID is invalid or missing.");
        
        var playlists = await _playlistService.GetAllPlaylistsForUser((Guid)userId);
        return Ok(playlists);
    }

    // GET: api/Playlist/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlaylistById(Guid id)
    {
        var playlist = await _playlistService.GetPlaylistById(id);
        if (playlist == null)
        {
            return NotFound($"Playlist with ID {id} not found.");
        }
        return Ok(playlist);
    }

    // POST: api/Playlist
    [HttpPost]
    public async Task<IActionResult> CreatePlaylist([FromBody] PlaylistDto playlistDto)
    {
        var userId = GetUserIdFromClaims(User);
    
        if (!userId.HasValue)
            return Unauthorized("User ID is invalid or missing.");
        
        var createdPlaylist = await _playlistService.CreatePlaylist(playlistDto, (Guid)userId);
        return CreatedAtAction(nameof(GetPlaylistById), new { id = createdPlaylist.Id }, createdPlaylist);
    }

    // PUT: api/Playlist/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlaylist(Guid id, [FromBody] PlaylistDto playlistDto)
    {
        var userId = GetUserIdFromClaims(User);
    
        if (!userId.HasValue)
            return Unauthorized("User ID is invalid or missing.");
        
        try
        {
            var updatedPlaylist = await _playlistService.UpdatePlaylist(id, playlistDto, userId.Value);
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("You do not have permission to update this playlist.");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    // DELETE: api/Playlist/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlaylist(Guid id)
    {
        var userId = GetUserIdFromClaims(User);
    
        if (!userId.HasValue)
            return Unauthorized("User ID is invalid or missing.");
        
        var playlist = await _playlistService.GetPlaylistById(id);
        if (playlist == null)
            return NotFound($"Playlist with ID {id} not found.");
        
        if (playlist.UserId != userId.Value)
            return Unauthorized("You do not have permission to delete this playlist.");

        await _playlistService.DeletePlaylist(id);
        return NoContent();
    }
    
    private static Guid? GetUserIdFromClaims(ClaimsPrincipal user)
    {
        var userIdClaim = user?.FindFirst("UserId")?.Value;
    
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return null;
        }
    
        return userId;
    }
}