namespace Cinema.Core.RequestFiltering;

public class SessionFilter
{
    public DateTime? After { get; set; }

    public DateTime? Until { get; set; }

    public Guid? AuditoriumId { get; set; }

    public Guid? MovieId { get; set; }
}