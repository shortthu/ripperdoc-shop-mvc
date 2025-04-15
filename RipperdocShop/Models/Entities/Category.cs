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
        Id = Guid.NewGuid();
        Name = name;
        Slug = SlugGenerator.GenerateSlug(name);
        Description = description;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    
    
}