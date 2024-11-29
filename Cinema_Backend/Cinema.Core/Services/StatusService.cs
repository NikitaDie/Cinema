using AutoMapper;
using Cinema.Core.DTOs.Branch;
using Cinema.Core.DTOs.Status;
using Cinema.Core.Helpers.UnifiedResponse;
using Cinema.Core.Interfaces;
using Cinema.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Core.Services;

public class StatusService : IStatusService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public StatusService(IRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<Result<ICollection<GetStatusDto>>> GetAllStatuses()
    {
        var statuses = _repository.GetAll<Status>()
            .Where(s => s.DeletedAt == null)
            .AsNoTracking();
        
        var resultList = await statuses.ToListAsync();
        var statusDtos = _mapper.Map<ICollection<GetStatusDto>>(resultList);
        return Result.Success(statusDtos);
    }

    public async Task<Result<GetStatusDto>> GetStatus(Guid id)
    {
        var status = await _repository.GetAll<Status>()
            .Where(s => s.DeletedAt == null)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (status == null)
            return Result.Failure<GetStatusDto>("Status not found.");
        
        var statusDto = _mapper.Map<GetStatusDto>(status);
        return Result.Success(statusDto);
    }

    public async Task<Result<GetStatusDto>> CreateStatus(CreateStatusDto newStatus)
    {
        var status = _mapper.Map<Status>(newStatus);
            await _repository.AddAsync(status);
            await _repository.SaveChangesAsync();
        
        return Result.Success(_mapper.Map<GetStatusDto>(status));
    }

    public async Task<Result<GetStatusDto>> UpdateStatus(Guid id, UpdateStatusDto statusToUpdate)
    {
        var status = await _repository.GetAll<Status>()
            .Where(s => s.DeletedAt == null)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (status == null)
            return Result.Failure<GetStatusDto>("Status not found.");
        
        _mapper.Map(statusToUpdate, status);
        await _repository.SaveChangesAsync();
        
        return Result.Success(_mapper.Map<GetStatusDto>(status));
    }

    public async Task<Result> DeleteStatus(Guid id)
    {
        var status = await _repository.GetAll<Status>()
            .Include(s => s.Pricelists)
            .Include(s => s.Seats)
                .ThenInclude(seat => seat.Tickets)
            .FirstOrDefaultAsync(s => s.Id == id);
        
        if (status == null)
            return Result.Failure("Status not found.");

        status.Pricelists.ToList().ForEach(pl => _repository.Delete(pl));
        status.Seats.ToList().ForEach(seat =>
        {
            seat.Tickets.ToList().ForEach(ticket => _repository.Delete(ticket));
            _repository.Delete(seat);
        });
        
        _repository.Delete(status);
        await _repository.SaveChangesAsync();
        
        return Result.Success();
    }
}