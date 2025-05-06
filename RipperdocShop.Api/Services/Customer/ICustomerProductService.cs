using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Services.Customer;

public interface ICustomerProductService
{
    Task<(IEnumerable<ProductDto> Products, int TotalCount, int TotalPages)> GetAllAsync(
        bool includeDeleted, int page, int pageSize);

    Task<ProductDto?> GetBySlugAsync(string slug);
    Task<IEnumerable<ProductDto>> GetFeaturedProductsAsync();

    Task<(IEnumerable<ProductDto> Products, int TotalCount, int TotalPages)>
        GetByCategorySlugAsync(string slug, bool includeDeleted, int page, int pageSize);

    Task<(IEnumerable<ProductDto> Products, int TotalCount, int TotalPages)>
        GetByBrandSlugAsync(string slug, bool includeDeleted, int page, int pageSize);
}
