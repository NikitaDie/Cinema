using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStreaming.Core.DTOs;
using MusicStreaming.Core.Interfaces;

namespace MusicStreaming.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SongController : ControllerBase
{
    private readonly ISongService _songService;

    public SongController(ISongService songService)
    {
        _songService = songService;
    }

    // POST: api/Song
    [HttpPost]
    public async Task<IActionResult> CreateSong([FromBody] SongDto songDto)
    {
        var userId = GetUserIdFromClaims(User);

        if (!userId.HasValue)
            return Unauthorized("User ID is invalid or missing.");
        
        var createdSong = await _songService.CreateSong(songDto, userId.Value);
        return CreatedAtAction(nameof(GetSongById), new { id = createdSong.Id }, createdSong);
    }

    // GET: api/Song/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSongById(Guid id)
    {
        var song = await _songService.GetSongById(id);
        if (song == null)
            return NotFound($"Song with ID {id} not found.");

        return Ok(song);
    }

    // DELETE: api/Song/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSong(Guid id)
    {
        var userId = GetUserIdFromClaims(User);

        if (!userId.HasValue)
            return Unauthorized("User ID is invalid or missing.");

        try
        {
            await _songService.DeleteSong(id, userId.Value);
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("You do not have permission to delete this song.");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    // POST: api/Song/{songId}/playlist/{playlistId}
    [HttpPost("{songId}/playlist/{playlistId}")]
    public async Task<IActionResult> AddSongToPlaylist(Guid songId, Guid playlistId)
    {
        var userId = GetUserIdFromClaims(User);
        if (!userId.HasValue)
            return Unauthorized("User ID is invalid or missing.");
            
        await _songService.AddSongToPlaylist(songId, playlistId, userId.Value);
        return Ok("Song added to playlist successfully.");
    }

    // DELETE: api/Song/{songId}/playlist/{playlistId}
    [HttpDelete("{songId}/playlist/{playlistId}")]
    public async Task<IActionResult> RemoveSongFromPlaylist(Guid songId, Guid playlistId)
    {
        var userId = GetUserIdFromClaims(User);
        if (!userId.HasValue)
            return Unauthorized("User ID is invalid or missing.");
            
        await _songService.RemoveSongFromPlaylist(songId, playlistId, userId.Value);
        return Ok("Song removed from playlist successfully.");
    }
    
    private static Guid? GetUserIdFromClaims(ClaimsPrincipal user)
    {
        var userIdString = user?.FindFirst("UserId")?.Value;
        if (Guid.TryParse(userIdString, out var userId))
        {
            return userId;
        }

        return null;
    }
}