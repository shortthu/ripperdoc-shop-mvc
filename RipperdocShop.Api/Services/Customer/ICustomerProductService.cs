using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Services.Customer;

public interface ICustomerProductService
{
    Task<ProductResponseDto> GetAllAsync(
        bool includeDeleted, int page, int pageSize);

    Task<ProductDto?> GetBySlugAsync(string slug);
    Task<IEnumerable<ProductDto>> GetFeaturedProductsAsync();

    Task<ProductResponseDto>
        GetByCategorySlugAsync(string slug, bool includeDeleted, int page, int pageSize);

    Task<ProductResponseDto>
        GetByBrandSlugAsync(string slug, bool includeDeleted, int page, int pageSize);
}
