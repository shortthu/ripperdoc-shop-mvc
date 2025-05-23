using RipperdocShop.Api.Models.Entities;

namespace RipperdocShop.Api.Models.DTOs;

public class AdminCategoryResponse
{
    public IEnumerable<Category> Categories { get; init; } = null!;
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
}
