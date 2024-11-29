using Cinema.Core.DTOs.Session;
using Cinema.Core.Helpers.UnifiedResponse;
using Cinema.Core.RequestFiltering;

namespace Cinema.Core.Interfaces;

public interface ISessionService
{
    public Task<Result<SessionDetailedDto>> GetSession(Guid id);
    public Task<Result<ICollection<SessionMinimalDto>>> GetAllSessions(SessionFilter filter, int skip, int take);
    public Task<Result<SessionDetailedDto>> CreateSession(CreateSessionDto newSession);
    // public Task<Result<GetSessionDto>> UpdateSession(Guid id, Session sessionToUpdate);
    public Task<Result> DeleteSession(Guid id);
}