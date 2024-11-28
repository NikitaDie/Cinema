using Cinema.Core.Models.Helpers;

namespace Cinema.Core.Models;

public partial class Auditorium : SoftDelete
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid BranchId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
