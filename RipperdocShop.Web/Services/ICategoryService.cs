using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public interface ICategoryService
{
    Task<CategoryDto?> GetBySlugAsync(string slug);

    Task<CategoryResponseDto?> GetAllAsync(bool includeDeleted = false,
        int page = 1, int pageSize = 10);
}
