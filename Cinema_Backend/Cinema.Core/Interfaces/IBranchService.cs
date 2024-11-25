using Cinema.Core.DTOs.Branch;
using Cinema.Core.Helpers.UnifiedResponse;
using Cinema.Core.RequestFiltering;

namespace Cinema.Core.Interfaces;

public interface IBranchService
{
    Task<Result<GetBranchDto>> GetBranch(Guid id);
    Task<Result<ICollection<GetBranchDto>>> GetAllBranches(BranchFilter filter, int skip, int take);
    Task<Result<GetBranchDto>> CreateBranch(CreateBranchDto newBranch);
    Task<Result<GetBranchDto>> UpdateBranch(Guid id, UpdateBranchDto branchToUpdate);
    Task<Result> DeleteBranch(Guid id);
}