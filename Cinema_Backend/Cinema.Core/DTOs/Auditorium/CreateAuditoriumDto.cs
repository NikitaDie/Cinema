namespace Cinema.Core.DTOs.Auditorium;

public class CreateAuditoriumDto
{
    public string Name { get; set; } = null!;

    public Guid BranchId { get; set; }
    
    public virtual ICollection<CreateSeatDto> Seats { get; set; } = new List<CreateSeatDto>();
}