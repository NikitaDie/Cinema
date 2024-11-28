using Cinema.Core.DTOs.Status;

namespace Cinema.Core.DTOs.Auditorium;

public class GetSeatDto
{
    public short Row { get; set; }

    public short Column { get; set; }

    public short XPosition { get; set; }

    public short YPosition { get; set; }

    public string Stasus { get; set; } = null!;
}