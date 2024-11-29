namespace Cinema.Core.DTOs.Auditorium;

public class CreateSeatDto
{
    public short Row { get; set; }

    public short Column { get; set; }

    public short XPosition { get; set; }

    public short YPosition { get; set; }

    public Guid StatusId { get; set; }
}