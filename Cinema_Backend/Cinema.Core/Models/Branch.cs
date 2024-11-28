using System;
using System.Collections.Generic;
using Cinema.Core.Models.Helpers;

namespace Cinema.Core.Models;

public partial class Branch : SoftDelete
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;
    
    public string City { get; set; } = null!;
    
    public string Region { get; set; } = null!;
    
    public string ZipCode { get; set; } = null!;
    
    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Auditorium> Auditoriums { get; set; } = new List<Auditorium>();
}
