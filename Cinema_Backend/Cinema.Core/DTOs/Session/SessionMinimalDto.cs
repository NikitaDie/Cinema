namespace Cinema.Core.DTOs.Session;

public class SessionMinimalDto
{
    public Guid Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public Guid AuditoriumId { get; set; }

    public Guid MovieId { get; set; }
}