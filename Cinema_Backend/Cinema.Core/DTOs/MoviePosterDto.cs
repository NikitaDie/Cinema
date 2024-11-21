namespace Cinema.Core.DTOs;

public class MoviePosterDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    
    public string? AssetsPath { get; set; }
}