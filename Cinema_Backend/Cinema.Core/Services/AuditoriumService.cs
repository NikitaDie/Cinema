using AutoMapper;
using Cinema.Core.DTOs.Auditorium;
using Cinema.Core.Helpers.UnifiedResponse;
using Cinema.Core.Interfaces;
using Cinema.Core.Models;
using Cinema.Core.RequestFiltering;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Core.Services;

public class AuditoriumService : IAuditoriumService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    
    public AuditoriumService(IRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Result<AuditoriumDetailedDto>> GetAuditorium(Guid id)
    {
        var auditorium = await _repository.GetAll<Auditorium>()
            .Where(a => a.DeletedAt == null)
            .Include(a => a.Seats)
                .ThenInclude(seat => seat.Status)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        if (auditorium == null)
            return Result.Failure<AuditoriumDetailedDto>("Auditorium not found");

        var auditoriumDto = _mapper.Map<AuditoriumDetailedDto>(auditorium);

        return Result.Success(auditoriumDto);
    }

    public async Task<Result<ICollection<AuditoriumMinimalDto>>> GetAllAuditoriums(AuditoriumFilter filter, int skip, int take)
    {
        var auditoriums = _repository.GetAll<Auditorium>()
            .Where(a => a.DeletedAt == null)
            .AsNoTracking();

        if (filter.BranchId is not null)
            auditoriums = auditoriums.Where(a => a.BranchId == filter.BranchId);

        if (filter.Name is not null)
            auditoriums = auditoriums.Where(a => a.Name.Contains(filter.Name));

        var resultList = await auditoriums.Skip(skip).Take(take).ToListAsync();
        var mappedResultList = _mapper.Map<ICollection<AuditoriumMinimalDto>>(resultList);

        return Result.Success(mappedResultList);
    }

    public async Task<Result<AuditoriumMinimalDto>> CreateAuditorium(CreateAuditoriumDto newAuditorium)
    {
        var auditorium = _mapper.Map<Auditorium>(newAuditorium);
        await _repository.AddAsync(auditorium);
        await _repository.SaveChangesAsync();
        
        var auditoriumDto = _mapper.Map<AuditoriumMinimalDto>(auditorium);
        
        return Result.Success(auditoriumDto);
    }
    
    public async Task<Result> DeleteAuditorium(Guid id)
    {
        var auditorium = await _repository.GetAll<Auditorium>()
            .Include(a => a.Seats)
                .ThenInclude(seat => seat.Tickets)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (auditorium == null)
            return Result.Failure<AuditoriumDetailedDto>("Auditorium not found");
        
        auditorium.Seats.ToList().ForEach(seat =>
        {
            seat.Tickets.ToList().ForEach(ticket => _repository.Delete(ticket));
            _repository.Delete(seat);
        });
        
        _repository.Delete(auditorium);
        await _repository.SaveChangesAsync();
        
        return Result.Success();
    }
}