using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public class BrandService(IHttpClientFactory factory) : BaseApiService(factory), IBrandService
{
    public Task<BrandDto?> GetBySlugAsync(string slug) =>
        GetAsync<BrandDto>($"/api/brands/{slug}");

    public Task<BrandResponseDto?> GetAllAsync(int page = 1, int pageSize = 10) =>
        GetAsync<BrandResponseDto>($"/api/brands/", new Dictionary<string, string>
        {
            { "page", page.ToString() },
            { "pageSize", pageSize.ToString() }
        });
}
