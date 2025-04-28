using RipperdocShop.Models.Entities;

namespace RipperdocShop.Models.DTOs;

public class ProductResponse
{
    public IEnumerable<Product> Products { get; init; } = null!;
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
}
