using Cinema.Core.Models;

namespace Cinema.Core.DTOs;

public class MovieDetailsDto
{
    public string Title { get; set; } = null!;
    
    public string? OriginalTitle { get; set; }
    
    public int AgeRating { get; set; }
    
    public int ReleaseYear { get; set; }
    
    public string Director { get; set; }
    
    public DateOnly RentalPeriodStart { get; set; }
    
    public DateOnly RentalPeriodEnd { get; set; }
    
    public string Language { get; set; }
    
    public List<GetGenreDto> Genres { get; set; }

    public TimeSpan Duration { get; set; }
    
    public string? Producer { get; set; }
    
    public string? ProductionStudio { get; set; }
    
    public string? Screenplay { get; set; }
    
    public List<GetActorDto> Starring { get; set; }
    
    public string? InclusiveAdaptation { get; set; }
    
    public string? Description { get; set; }

    public string TrailerLink { get; set; }
    
    public string ImageUri { get; set; }
}