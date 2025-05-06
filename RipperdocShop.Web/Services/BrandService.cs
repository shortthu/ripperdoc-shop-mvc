using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public class BrandService(IHttpClientFactory factory) : BaseApiService(factory), IBrandService
{
    public Task<BrandDto?> GetBySlugAsync(string slug) =>
        GetAsync<BrandDto>($"/api/brands/slug/{slug}");

    public Task<BrandResponseDto?> GetAllAsync() =>
        GetAsync<BrandResponseDto>($"/api/brands/");
}
