namespace RipperdocShop.Shared.DTOs;

public class BrandResponse
{
    public IEnumerable<BrandDto> Brands { get; init; } = null!;
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
}
