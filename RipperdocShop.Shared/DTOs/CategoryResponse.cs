namespace RipperdocShop.Shared.DTOs;

public class CategoryResponse
{
    public IEnumerable<CategoryDto> Categories { get; init; } = null!;
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
}
