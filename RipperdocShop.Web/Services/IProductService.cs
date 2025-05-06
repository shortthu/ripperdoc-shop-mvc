using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public interface IProductService
{
    Task<ProductDto?> GetBySlugAsync(string slug);
    Task<(List<ProductDto> Products, int TotalCount, int TotalPages)> GetAllAsync();
    Task<(List<ProductDto> Products, int TotalCount, int TotalPages)> GetByCategorySlugAsync(string slug);
    Task<(List<ProductDto> Products, int TotalCount, int TotalPages)> GetByBrandSlugAsync(string slug);
    Task<List<ProductDto>?> GetFeatured();
}
