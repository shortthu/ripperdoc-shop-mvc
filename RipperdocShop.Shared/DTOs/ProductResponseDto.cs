namespace RipperdocShop.Shared.DTOs;

public class ProductResponseDto
{
    public IEnumerable<ProductDto> Products { get; set; } = null!;
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
