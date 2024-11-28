using System;
using System.Collections.Generic;
using Cinema.Core.Models.Helpers;

namespace Cinema.Core.Models;

public partial class Ticket : SoftDelete
{
    public Guid Id { get; set; }

    public Guid SessionId { get; set; }

    public Guid SeatId { get; set; }

    public Guid ClientId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Seat Seat { get; set; } = null!;

    public virtual Session Session { get; set; } = null!;
}
