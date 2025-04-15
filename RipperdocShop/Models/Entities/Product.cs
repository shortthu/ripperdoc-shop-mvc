using RipperdocShop.Utils;

namespace RipperdocShop.Models.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string ImageUrl { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    
    public Guid? BrandId { get; private set; }
    public Brand? Brand { get; private set; }
    
    private Product() { }

    public Product(string name, string description, string imageUrl, decimal price, Category category, 
        Brand? brand = null)
    {
        Id = Guid.NewGuid();
        Name = name.Trim();
        Slug = SlugGenerator.GenerateSlug(name);
        Description = description.Trim();
        ImageUrl = imageUrl;
        Price = price;
        CategoryId = category.Id;
        Category = category;
        BrandId = brand?.Id;
        Brand = brand;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string name, string description, string imageUrl, decimal price, Category category,
        Brand? brand = null)
    {
        if (string.IsNullOrEmpty(name.Trim()))
            throw new ArgumentException("Name cannot be null or empty.");
        
        // TODO: More validation logic here
        
        Name = name.Trim();
        Slug = SlugGenerator.GenerateSlug(name);
        Description = description.Trim();
        ImageUrl = imageUrl;
        Price = price;
        CategoryId = category.Id;
        Category = category;
        BrandId = brand?.Id;
        Brand = brand;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void SoftDelete()
    {
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Restore()
    {
        DeletedAt = null;
        UpdatedAt = DateTime.UtcNow;
    }
}