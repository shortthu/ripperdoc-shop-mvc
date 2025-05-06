using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public class ProductRatingService(IHttpClientFactory factory) : BaseApiService(factory), IProductRatingService
{
    public Task<List<ProductRatingDto>?> GetByProductSlug(string slug, bool includeDeleted = false,
        int page = 1, int pageSize = 10) =>
        GetAsync<List<ProductRatingDto>?>($"/api/ratings/by-product/{slug}", new Dictionary<string, string>
        {
            { "includeDeleted", includeDeleted.ToString() },
            { "page", page.ToString() },
            { "pageSize", pageSize.ToString() }
        });
}
