using System;
using System.Collections.Generic;

namespace Cinema.Core.Models;

public partial class Branch
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Auditorium> Auditoriums { get; set; } = new List<Auditorium>();
}
