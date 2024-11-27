namespace Cinema.Core.Models;

public partial class Seat
{
    public Guid Id { get; set; }

    public short Row { get; set; }

    public short Column { get; set; }

    public short XPosition { get; set; }

    public short YPosition { get; set; }

    public Guid AuditoriumId { get; set; }

    public Guid StatusId { get; set; }

    public virtual Auditorium Auditorium { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
