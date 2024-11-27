using Cinema.Core.DTOs.Status;
using Cinema.Core.Helpers.UnifiedResponse;

namespace Cinema.Core.Interfaces;

public interface IStatusService
{
    public Task<Result<ICollection<GetStatusDto>>> GetAllStatuses();
    public Task<Result<GetStatusDto>> GetStatus(Guid id);
    public Task<Result<GetStatusDto>> CreateStatus(CreateStatusDto newStatus);
    public Task<Result<GetStatusDto>> UpdateStatus(Guid id, UpdateStatusDto statusToUpdate);
    public Task<Result> DeleteStatus(Guid id);
}