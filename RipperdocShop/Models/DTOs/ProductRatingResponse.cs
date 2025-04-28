using RipperdocShop.Models.Entities;

namespace RipperdocShop.Models.DTOs;

public class ProductRatingResponse
{
    public IEnumerable<ProductRating> Ratings { get; init; } = null!;
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
}
