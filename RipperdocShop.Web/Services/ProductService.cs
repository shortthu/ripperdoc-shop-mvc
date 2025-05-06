using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public class ProductService(IHttpClientFactory factory) : BaseApiService(factory), IProductService
{
    public Task<ProductDto?> GetBySlugAsync(string slug) =>
        GetAsync<ProductDto>($"products/slug/{slug}");
    
    public Task<(List<ProductDto> Products, int TotalCount, int TotalPages)> GetAllAsync() =>
        GetAsync<(List<ProductDto> Products, int TotalCount, int TotalPages)>($"products/");

    public Task<(List<ProductDto> Products, int TotalCount, int TotalPages)> GetByCategorySlugAsync(string slug) =>
        GetAsync<(List<ProductDto> Products, int TotalCount, int TotalPages)>($"products/category/{slug}");

    public Task<(List<ProductDto> Products, int TotalCount, int TotalPages)> GetByBrandSlugAsync(string slug) =>
        GetAsync<(List<ProductDto> Products, int TotalCount, int TotalPages)>($"products/brand/{slug}");

    public Task<List<ProductDto>?> GetFeatured() =>
        GetAsync<List<ProductDto>>($"products/featured");
}
