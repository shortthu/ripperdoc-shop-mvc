using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Services.Customer;

public interface ICustomerProductService
{
    Task<(IEnumerable<ProductResponseDto> Products, int TotalCount, int TotalPages)> GetAllAsync(
        bool includeDeleted, int page, int pageSize);

    Task<ProductResponseDto?> GetBySlugAsync(string slug);
    Task<IEnumerable<ProductResponseDto>> GetFeaturedProductsAsync();
}
