using Cinema.Core.Helpers.Validation;

namespace Cinema.Core.DTOs.Session;

public class CreateSessionDto : BaseValidationModel<CreateSessionDto>
{
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public Guid AuditoriumId { get; set; }

    public Guid MovieId { get; set; }

    public virtual ICollection<CreatePricelistDto> Pricelists { get; set; } = new List<CreatePricelistDto>();
    
}