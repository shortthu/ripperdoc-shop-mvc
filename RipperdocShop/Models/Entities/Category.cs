using RipperdocShop.Utils;

namespace RipperdocShop.Models.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    
    private Category() { }

    public Category(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentException("It's got a name y'know (Category name is required)");
        
        Id = Guid.NewGuid();
        Name = name.Trim();
        Slug = SlugGenerator.GenerateSlug(name);
        Description = description.Trim();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string name, string description)
    {
        if (DeletedAt != null)
            throw new InvalidOperationException("Playing with ghosts, are we? (Cannot update a deleted category)");
        if (string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentException("It's got a name y'know (Category name is required)");
        
        Name = name.Trim();
        Slug = SlugGenerator.GenerateSlug(name);
        Description = description.Trim();
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void SoftDelete()
    {if (DeletedAt != null)
            throw new InvalidOperationException("Already flatlined, choom. (Category is already deleted)");
        
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Restore()
    {
        if (DeletedAt == null)
            throw new InvalidOperationException("It's still alive, y'know. (Category is not deleted)");
        
        DeletedAt = null;
        UpdatedAt = DateTime.UtcNow;
    }
}