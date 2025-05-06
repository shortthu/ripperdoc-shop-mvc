using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public class CategoryService(IHttpClientFactory factory) : BaseApiService(factory), ICategoryService
{
    public Task<CategoryDto?> GetBySlugAsync(string slug) =>
        GetAsync<CategoryDto>($"categories/slug/{slug}");

    public Task<(List<CategoryDto> Products, int TotalCount, int TotalPages)> GetAllAsync() =>
        GetAsync<(List<CategoryDto> Products, int TotalCount, int TotalPages)>($"categories/");
}
