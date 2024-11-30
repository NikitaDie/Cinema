using Cinema.Core.DTOs.Ticket;
using Cinema.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_Backend.Controllers;

[Route("api/tickets")]
[ApiController]
public class TicketController : ControllerBase
{
    private ITicketService _ticketService;
    
    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }
    
    // GET: api/tickets
    [HttpGet]
    public async Task<IActionResult> GetAllTickets()
    {
        var result = await _ticketService.GetAllTickets();
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
    
    // GET: api/tickets/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTicket([FromRoute] Guid id)
    {
        var result = await _ticketService.GetTicket(id);
        return result.IsSuccess
            ? Ok(result)
            : NotFound(result.Error);
    }
    
    // POST: api/tickets
    [HttpPost]
    public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto newTicket)
    {
        var result = await _ticketService.CreateTicket(newTicket);
        var createdStatusId = result.Data?.Id;
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetTicket), new { id = createdStatusId }, result)
            : BadRequest(result.Error);
    }
    
    // DELETE: api/tickets/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTicket([FromRoute] Guid id)
    {
        var result = await _ticketService.DeleteTicket(id);
        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result.Error);
    }
}