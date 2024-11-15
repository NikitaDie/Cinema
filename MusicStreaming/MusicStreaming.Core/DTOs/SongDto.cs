namespace MusicStreaming.Core.DTOs;

public class SongDto
{
    public string Title { get; set; } = null!;
    public string Artist { get; set; } = null!;
    public string Album { get; set; } = null!;
    
    public int DurationInSeconds { get; set; }
    
    public TimeSpan Duration { get; set; }

    public string? Genre { get; set; }

    public string Url { get; set; } = null!;
}