using Cinema.Core.DTOs.Auditorium;
using Cinema.Core.Interfaces;
using Cinema.Core.RequestFiltering;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_Backend.Controllers;

[Route("api/auditoriums")]
[ApiController]
public class AuditoriumController : ControllerBase
{
    private readonly IAuditoriumService _auditoriumService;

    public AuditoriumController(IAuditoriumService auditoriumService)
    {
        _auditoriumService = auditoriumService;
    }
    
     //Get api/auditoriums/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAuditorium([FromRoute] Guid id)
    {
        var result = await _auditoriumService.GetAuditorium(id);
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
    
    // GET: api/auditoriums
    [HttpGet]
    public async Task<IActionResult> GetAllAuditoriums(
        [FromQuery] string? name,
        [FromQuery] Guid? branchId,
        [FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        var filter = new AuditoriumFilter
        {
            Name = name,
            BranchId = branchId
        };
        
        var result = await _auditoriumService.GetAllAuditoriums(filter, skip, take);
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
    
    // POST: api/auditoriums/{id}
    [HttpPost]
    public async Task<IActionResult> CreateAuditorium([FromBody] CreateAuditoriumDto newAuditorium)
    {
        var result = await _auditoriumService.CreateAuditorium(newAuditorium);
        var createdAuditoriumId = result.Data?.Id;
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetAuditorium), new { id = createdAuditoriumId }, result)
            : NotFound(result.Error);
    }
    
    
    // DELETE: api/auditoriums/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAuditorium([FromRoute] Guid id)
    {
        var result = await _auditoriumService.DeleteAuditorium(id);
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
}