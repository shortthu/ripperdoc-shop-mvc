using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public interface ICategoryService
{
    Task<CategoryDto?> GetBySlugAsync(string slug);
    Task<CategoryResponseDto?> GetAllAsync();
}
