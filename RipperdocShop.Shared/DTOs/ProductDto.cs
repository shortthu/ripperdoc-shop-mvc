namespace RipperdocShop.Shared.DTOs;

public class ProductDto
{
    public string Name { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public bool IsFeatured { get; init; } = false;
    public CategoryDto Category { get; init; } = null!;
    public BrandCreateDto? Brand { get; init; }
}
