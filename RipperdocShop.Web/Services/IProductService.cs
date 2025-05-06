using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public interface IProductService
{
    Task<ProductDto?> GetBySlugAsync(string slug);
    Task<ProductResponseDto?> GetAllAsync();
    Task<ProductResponseDto?> GetByCategorySlugAsync(string slug);
    Task<ProductResponseDto?> GetByBrandSlugAsync(string slug);
    Task<List<ProductDto>?> GetFeaturedAsync();
}
