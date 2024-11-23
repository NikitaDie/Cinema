using Microsoft.AspNetCore.Http;

namespace Cinema.Core.DTOs;

public class MovieMinimalDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    
    public string ImageUri { get; set; }
}