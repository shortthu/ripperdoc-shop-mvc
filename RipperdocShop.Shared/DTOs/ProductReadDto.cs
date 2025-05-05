namespace RipperdocShop.Shared.DTOs;

public class ProductReadDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsFeatured { get; set; } = false;
    public CategoryDto Category { get; set; } = null!;
    public BrandDto? Brand { get; set; }
}
