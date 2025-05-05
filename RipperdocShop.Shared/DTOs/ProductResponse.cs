namespace RipperdocShop.Shared.DTOs;

public class ProductResponse
{
    public IEnumerable<ProductResponseDto> Products { get; init; } = null!;
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
}
