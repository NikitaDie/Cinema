using System;
using System.Collections.Generic;
using Cinema.Core.Models.Helpers;

namespace Cinema.Core.Models;

public partial class Client : SoftDelete
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
