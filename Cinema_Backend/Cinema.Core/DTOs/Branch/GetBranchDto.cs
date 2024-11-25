using Cinema.Core.DTOs.Auditorium;

namespace Cinema.Core.DTOs.Branch;

public class GetBranchDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;
    
    public string City { get; set; } = null!;
    
    public string Region { get; set; } = null!;
    
    public string ZipCode { get; set; } = null!;
    
    public string PhoneNumber { get; set; } = null!;
    
    public virtual ICollection<AuditoriumMinimalDto> Auditoriums { get; set; } = new List<AuditoriumMinimalDto>();
}