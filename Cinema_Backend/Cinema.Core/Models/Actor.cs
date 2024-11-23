namespace Cinema.Core.Models;

public class Actor
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public string FullName => $"{FirstName} {LastName}";
    
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}