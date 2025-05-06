using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public class ProductService(IHttpClientFactory factory) : BaseApiService(factory), IProductService
{
    public Task<ProductDto?> GetBySlugAsync(string slug) =>
        GetAsync<ProductDto>($"/api/products/slug/{slug}");

    public Task<ProductResponseDto?> GetAllAsync() =>
        GetAsync<ProductResponseDto?>($"/api/products/");

    public Task<ProductResponseDto?> GetByCategorySlugAsync(string slug) =>
        GetAsync<ProductResponseDto?>($"/api/products/category/{slug}");

    public Task<ProductResponseDto?> GetByBrandSlugAsync(string slug) =>
        GetAsync<ProductResponseDto?>($"/api/products/brand/{slug}");

    public Task<List<ProductDto>?> GetFeaturedAsync() =>
        GetAsync<List<ProductDto>>($"/api/products/featured");
}
