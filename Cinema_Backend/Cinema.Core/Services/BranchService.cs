using AutoMapper;
using Cinema.Core.DTOs.Branch;
using Cinema.Core.Helpers.UnifiedResponse;
using Cinema.Core.Interfaces;
using Cinema.Core.Models;
using Cinema.Core.RequestFiltering;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Core.Services;

public class BranchService : IBranchService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public BranchService(IRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<Result<GetBranchDto>> GetBranch(Guid id)
    {
        var branch = await _repository.GetAll<Branch>()
            .Include(b => b.Auditoriums)
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);

        if (branch == null)
            return Result.Failure<GetBranchDto>("Branch not found.");

        var branchDto = _mapper.Map<GetBranchDto>(branch);

        return Result.Success(branchDto);
    }

    public async Task<Result<ICollection<GetBranchDto>>> GetAllBranches(BranchFilter filter, int skip, int take)
    {
        var branches  = _repository.GetAll<Branch>().AsNoTracking();

        if (filter.Address is not null)
            branches = branches.Where(b => b.Address.ToLower() == filter.Address.ToLower());
        
        if (filter.City is not null)
            branches = branches.Where(b =>b.City.ToLower() == filter.City.ToLower());
        
        if (filter.Name is not null)
            branches = branches.Where(b => b.Name.ToLower() == filter.Name.ToLower());
        
        if (filter.PhoneNumber is not null)
            branches = branches.Where(b => b.PhoneNumber.ToLower() == filter.PhoneNumber.ToLower());
        
        if (filter.Region is not null)
            branches = branches.Where(b => b.Region.ToLower() == filter.Region.ToLower());
        
        if (filter.ZipCode is not null)
            branches = branches.Where(b => b.ZipCode.ToLower() == filter.ZipCode.ToLower());
        
        var resultList = await branches.Skip(skip).Take(take).ToListAsync();
        var mappedResultList = _mapper.Map<ICollection<GetBranchDto>>(resultList);
        return Result.Success(mappedResultList);
    }

    public async Task<Result<GetBranchDto>> CreateBranch(CreateBranchDto newBranch)
    {
        var branch = _mapper.Map<Branch>(newBranch);
        await _repository.AddAsync(branch);
        await _repository.SaveChangesAsync();
        
        return Result.Success(_mapper.Map<GetBranchDto>(branch));
    }

    public async Task<Result<GetBranchDto>> UpdateBranch(Guid id, UpdateBranchDto branchToUpdate)
    {
        var branch = await _repository.GetAll<Branch>()
            .FirstOrDefaultAsync(b => b.Id == id);

        if (branch == null)
            return Result.Failure<GetBranchDto>("Branch not found.");
        
        _mapper.Map(branchToUpdate, branch);
        await _repository.SaveChangesAsync();
        
        return Result.Success(_mapper.Map<GetBranchDto>(branch));
    }

    public async Task<Result> DeleteBranch(Guid id)
    {
        var branch = await _repository.GetAll<Branch>()
            .FirstOrDefaultAsync(b => b.Id == id);
        
        if (branch == null)
            return Result.Failure("Branch not found.");

        _repository.Delete(branch);
        await _repository.SaveChangesAsync();
        
        return Result.Success();
    }
}