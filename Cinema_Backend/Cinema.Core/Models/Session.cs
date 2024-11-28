using System;
using System.Collections.Generic;
using Cinema.Core.Models.Helpers;

namespace Cinema.Core.Models;

public partial class Session : SoftDelete
{
    public Guid Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public Guid AuditoriumId { get; set; }

    public Guid MovieId { get; set; }

    public virtual Auditorium Auditorium { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;

    public virtual ICollection<Pricelist> Pricelists { get; set; } = new List<Pricelist>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
