using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Services.Customer;

public interface ICustomerBrandService
{
    Task<BrandResponseDto> GetAllAsync(
        bool includeDeleted, int page, int pageSize);

    Task<BrandDto?> GetBySlugAsync(string slug);
}
