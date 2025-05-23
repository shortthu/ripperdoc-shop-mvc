using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Services.Customer;

public interface ICustomerCategoryService
{
    Task<PaginatedCategoryResponse> GetAllAsync(
        bool includeDeleted, int page, int pageSize);

    Task<CategoryDto?> GetBySlugAsync(string slug);
}
