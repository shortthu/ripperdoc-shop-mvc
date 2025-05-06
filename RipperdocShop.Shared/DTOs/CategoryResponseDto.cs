namespace RipperdocShop.Shared.DTOs;

public class CategoryResponseDto
{
    public IEnumerable<CategoryDto> Categories { get; set; } = null!;
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
