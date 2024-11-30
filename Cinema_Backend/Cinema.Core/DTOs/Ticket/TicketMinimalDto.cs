namespace Cinema.Core.DTOs.Ticket;

public class TicketMinimalDto
{
    public Guid Id { get; set; }

    public Guid SessionId { get; set; }

    public Guid SeatId { get; set; }

    public Guid ClientId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}