using Cinema.Core.DTOs;
using Cinema.Core.DTOs.Ticket;
using Cinema.Core.Helpers.UnifiedResponse;

namespace Cinema.Core.Interfaces;

public interface ITicketService
{
    public Task<Result<ICollection<TicketMinimalDto>>> GetAllTickets();
    
    Task<Result<TicketMinimalDto>> CreateTicket(CreateTicketDto createMovieDto);
    Task<Result<TicketDetailedDto>> GetTicket(Guid id);
    Task<Result> DeleteTicket(Guid id);
}