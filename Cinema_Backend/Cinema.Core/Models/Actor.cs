using Cinema.Core.Models.Helpers;

namespace Cinema.Core.Models;

public class Actor : SoftDelete
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public string FullName => $"{FirstName} {LastName}";
    
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}