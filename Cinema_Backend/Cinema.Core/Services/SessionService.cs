using AutoMapper;
using Cinema.Core.DTOs.Session;
using Cinema.Core.Helpers.UnifiedResponse;
using Cinema.Core.Interfaces;
using Cinema.Core.Models;
using Cinema.Core.RequestFiltering;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Core.Services;

public class SessionService : ISessionService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public SessionService(IRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public Task<Result<ICollection<SessionDetailedDto>>> GetAllSessions()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<SessionDetailedDto>> GetSession(Guid id)
    {
        var session = await _repository.GetAll<Session>()
            .Include(s => s.Pricelists)
                .ThenInclude(pl => pl.Status)
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);
        
        if (session == null)
            return Result.Failure<SessionDetailedDto>("Session not found.");
        
        var sessionDto = _mapper.Map<SessionDetailedDto>(session);

        return Result.Success(sessionDto);
    }

    public async Task<Result<ICollection<SessionMinimalDto>>> GetAllSessions(SessionFilter filter, int skip, int take)
    {
        var sessions = _repository.GetAll<Session>().AsNoTracking();
        
        if (filter.After is not null)
            sessions = sessions.Where(s => s.StartTime >= filter.After);
        
        if (filter.Until is not null)
            sessions = sessions.Where(s => s.EndTime <= filter.Until);
        
        if (filter.AuditoriumId is not null)
            sessions = sessions.Where(s => s.AuditoriumId == filter.AuditoriumId);
        
        if (filter.MovieId is not null)
            sessions = sessions.Where(s => s.MovieId == filter.MovieId);
        
        var resultList = await sessions.Skip(skip).Take(take).ToListAsync();
        var mappedResultList = _mapper.Map<ICollection<SessionMinimalDto>>(resultList);
        return Result.Success(mappedResultList);
    }

    public async Task<Result<SessionDetailedDto>> CreateSession(CreateSessionDto newSession)
    {
        var session = _mapper.Map<Session>(newSession);
        await _repository.AddAsync(session);
        await _repository.SaveChangesAsync();
        
        var sessionDto = _mapper.Map<SessionDetailedDto>(session);
        return Result.Success(sessionDto);
    }

    public async Task<Result> DeleteSession(Guid id)
    {
        var session = await _repository.GetAll<Session>()
            .Include(s => s.Tickets)
            .Include(s => s.Pricelists)
            .FirstOrDefaultAsync(b => b.Id == id);
        
        if (session == null)
            return Result.Failure<SessionDetailedDto>("Session not found.");
        
        session.Pricelists.ToList().ForEach(pl => _repository.Delete(pl));
        session.Tickets.ToList().ForEach(pl => _repository.Delete(pl));
        
        _repository.Delete(session);
        await _repository.SaveChangesAsync();
        
        return Result.Success();
    }
}