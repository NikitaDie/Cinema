using Cinema.Core.Models.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Cinema.Storage.Utils;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        if (eventData.Context is null) return result;
        
        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Deleted, Entity: SoftDelete delete }) continue;
            entry.State = EntityState.Modified;
            delete.IsDeleted = true;
            delete.DeletedAt = DateTimeOffset.UtcNow;
        }
        return result;
    }
}