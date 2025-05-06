using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Services.Customer;

public interface ICustomerBrandService
{
    Task<(IEnumerable<BrandDto> Categories, int TotalCount, int TotalPages)> GetAllAsync(
        bool includeDeleted, int page, int pageSize);

    Task<BrandDto?> GetBySlugAsync(string slug);
}
