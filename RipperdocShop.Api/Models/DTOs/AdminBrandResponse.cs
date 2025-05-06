using RipperdocShop.Api.Models.Entities;

namespace RipperdocShop.Api.Models.DTOs;

public class AdminBrandResponse
{
    public IEnumerable<Brand> Brands { get; init; } = null!;
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
}
