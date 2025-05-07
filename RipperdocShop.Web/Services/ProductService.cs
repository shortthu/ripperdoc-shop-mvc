using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public class ProductService(IHttpClientFactory factory) : BaseApiService(factory), IProductService
{
    public Task<ProductDto?> GetBySlugAsync(string slug) =>
        GetAsync<ProductDto>($"/api/products/{slug}");

    public Task<ProductResponseDto?> GetAllAsync(int page = 1, int pageSize = 10) =>
        GetAsync<ProductResponseDto?>($"/api/products/", new Dictionary<string, string>
        {
            { "page", page.ToString() },
            { "pageSize", pageSize.ToString() }
        });

    public Task<ProductResponseDto?> GetByCategorySlugAsync(string slug) =>
        GetAsync<ProductResponseDto?>($"/api/products/category/{slug}");

    public Task<ProductResponseDto?> GetByBrandSlugAsync(string slug) =>
        GetAsync<ProductResponseDto?>($"/api/products/brand/{slug}");

    public Task<List<ProductDto>?> GetFeaturedAsync() =>
        GetAsync<List<ProductDto>>($"/api/products/featured");
}
