namespace RipperdocShop.Shared.DTOs;

public class ProductRatingResponseDto
{
    public IEnumerable<ProductRatingDto> Ratings { get; set; } = null!;
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
