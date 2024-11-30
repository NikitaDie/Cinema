using Cinema.Core.DTOs.Session;
using Cinema.Core.Interfaces;
using Cinema.Core.RequestFiltering;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_Backend.Controllers;

[Route("api/sessions")]
[ApiController]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }
    
    //Get api/sessions/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSession([FromRoute] Guid id)
    {
        var result = await _sessionService.GetSession(id);
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
    
    // GET: api/sessions
    [HttpGet]
    public async Task<IActionResult> GetAllSessions(
        [FromQuery] DateTime? after,
        [FromQuery] DateTime? until,
        [FromQuery] Guid? auditoriumId,
        [FromQuery] Guid? movieId,
        [FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        var filter = new SessionFilter
        {
            After = after,
            Until = until,
            AuditoriumId = auditoriumId,
            MovieId = movieId
        };
        
        var result = await _sessionService.GetAllSessions(filter, skip, take);
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
    
    // POST: api/sessions/{id}
    [HttpPost]
    public async Task<IActionResult> CreateBranch([FromBody] CreateSessionDto newSession)
    {
        var result = await _sessionService.CreateSession(newSession);
        var createdSessionId = result.Data?.Id;
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetSession), new { id = createdSessionId }, result)
            : NotFound(result.Error);
    }
    
    // DELETE: api/sessions/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSession([FromRoute] Guid id)
    {
        var result = await _sessionService.DeleteSession(id);
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
}