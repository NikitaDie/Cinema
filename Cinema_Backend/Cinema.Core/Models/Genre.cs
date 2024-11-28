using Cinema.Core.Models.Helpers;

namespace Cinema.Core.Models;

public class Genre : SoftDelete
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}