using RipperdocShop.Api.Models.Entities;
using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Models.DTOs;

public class AdminProductResponse
{
    public IEnumerable<Product> Products { get; init; } = null!;
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
}
