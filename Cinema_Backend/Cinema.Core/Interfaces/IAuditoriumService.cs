using Cinema.Core.DTOs.Auditorium;
using Cinema.Core.Helpers.UnifiedResponse;
using Cinema.Core.RequestFiltering;

namespace Cinema.Core.Interfaces;

public interface IAuditoriumService
{
    Task<Result<AuditoriumMinimalDto>> CreateAuditorium(CreateAuditoriumDto newAuditorium);
    Task<Result<AuditoriumDetailedDto>> GetAuditorium(Guid id);
    Task<Result<ICollection<AuditoriumMinimalDto>>> GetAllAuditoriums(AuditoriumFilter filter, int skip, int take);
    Task<Result> DeleteAuditorium(Guid id);
}