using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public interface ICategoryService
{
    Task<CategoryDto?> GetBySlugAsync(string slug);
    Task<(List<CategoryDto> Products, int TotalCount, int TotalPages)> GetAllAsync();
}
