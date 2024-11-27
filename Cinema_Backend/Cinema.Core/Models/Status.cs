namespace Cinema.Core.Models;

public partial class Status
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Pricelist> Pricelists { get; set; } = new List<Pricelist>();

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
