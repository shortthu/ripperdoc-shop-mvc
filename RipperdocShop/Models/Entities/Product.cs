using System.ComponentModel.DataAnnotations;
using RipperdocShop.Utils;

namespace RipperdocShop.Models.Entities;

public class Product
{
    public Guid Id { get; private set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; private set; } = string.Empty;
    
    [Required]
    [StringLength(120)]
    public string Slug { get; private set; } = string.Empty;
    
    [Required]
    [StringLength(500)]
    public string Description { get; private set; } = string.Empty;
    
    [Required]
    [Url]
    public string ImageUrl { get; private set; } = string.Empty;
    
    [Range(0.0, double.MaxValue)]
    public decimal Price { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    
    [Required]
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    
    public Guid? BrandId { get; private set; }
    public Brand? Brand { get; private set; }
    
    private Product() { }

    public Product(string name, string description, string imageUrl, decimal price, Category category, 
        Brand? brand = null)
    {
        if (string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentException("You tryna sell nothing? (Product name is required)");
        if (price < 0) throw new ArgumentException("You can't sell debt, choom. (Price cannot be negative)");
        
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
        if (DeletedAt != null)
            throw new InvalidOperationException("Playing with ghosts, are we? (Cannot update a deleted product)");
        if (string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentException("You tryna sell nothing? (Product name is required)");
        if (price < 0) throw new ArgumentException("You can't sell debt, choom. (Price cannot be negative)");

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
        if (DeletedAt != null)
            throw new InvalidOperationException("Already flatlined, choom. (Product is already deleted)");
        
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Restore()
    {
        if (DeletedAt == null)
            throw new InvalidOperationException("It's still alive, y'know. (Product is not deleted)");
        
        DeletedAt = null;
        UpdatedAt = DateTime.UtcNow;
    }
}