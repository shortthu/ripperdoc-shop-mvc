namespace RipperdocShop.Shared.DTOs;

public class CategoryResponse
{
    public IEnumerable<CategoryDto> Categories { get; set; } = null!;
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
