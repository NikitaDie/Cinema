using Cinema.Core.DTOs.Auditorium;
using Cinema.Core.Helpers.UnifiedResponse;

namespace Cinema.Core.Interfaces;

public interface IAuditoriumService
{
    Task<Result<AuditoriumMinimalDto>> CreateAuditorium(CreateAuditoriumDto newAuditorium);
    Task<Result<AuditoriumDetailsDto>> GetAuditorium(Guid id);
}