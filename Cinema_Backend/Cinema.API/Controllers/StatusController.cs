using Cinema.Core.DTOs.Status;
using Cinema.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_Backend.Controllers;

[Route("api/statuses")]
[ApiController]
public class StatusController : ControllerBase
{
    private IStatusService _statusService;
    
    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
    }
    
    // GET: api/statuses
    [HttpGet]
    public async Task<IActionResult> GetAllStatuses()
    {
        var result = await _statusService.GetAllStatuses();
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
    
    // GET: api/statuses/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetStatus([FromRoute] Guid id)
    {
        var result = await _statusService.GetStatus(id);
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
    
    // POST: api/statuses
    [HttpPost]
    public async Task<IActionResult> CreateStatus([FromBody] CreateStatusDto newStatus)
    {
        var result = await _statusService.CreateStatus(newStatus);
        var createdStatusId = result.Data?.Id;
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetStatus), new { id = createdStatusId }, result)
            : BadRequest(result.Error);
    }
    
    // PUT: api/statuses/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] UpdateStatusDto updatedStatus)
    {
        var result = await _statusService.UpdateStatus(id, updatedStatus);
        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result.Error);
    }
    
    // DELETE: api/statuses/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteStatus([FromRoute] Guid id)
    {
        var result = await _statusService.DeleteStatus(id);
        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result.Error);
    }
}