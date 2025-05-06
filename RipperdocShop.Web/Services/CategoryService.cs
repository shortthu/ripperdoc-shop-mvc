using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public class CategoryService(IHttpClientFactory factory) : BaseApiService(factory), ICategoryService
{
    public Task<CategoryDto?> GetBySlugAsync(string slug) =>
        GetAsync<CategoryDto>($"/api/categories/slug/{slug}");

    public Task<CategoryResponseDto?> GetAllAsync() =>
        GetAsync<CategoryResponseDto>($"/api/categories",
            new Dictionary<string, string>
            {
                { "includeDeleted", "false" }
            });
}
