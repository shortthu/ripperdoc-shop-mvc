using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public class BrandService(IHttpClientFactory factory) : BaseApiService(factory), IBrandService
{
    public Task<BrandDto?> GetBySlugAsync(string slug) =>
        GetAsync<BrandDto>($"brands/slug/{slug}");

    public Task<(List<BrandDto> Products, int TotalCount, int TotalPages)> GetAllAsync() =>
        GetAsync<(List<BrandDto> Products, int TotalCount, int TotalPages)>($"brands/");
}
