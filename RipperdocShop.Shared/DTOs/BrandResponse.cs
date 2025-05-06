namespace RipperdocShop.Shared.DTOs;

public class BrandResponse
{
    public IEnumerable<BrandDto> Brands { get; set; } = null!;
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
