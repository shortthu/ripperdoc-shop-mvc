using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Services;

public interface IProductRatingService
{
    Task<List<ProductRatingDto>?> GetByProductSlug(string slug, bool includeDeleted = false,
        int page = 1, int pageSize = 10);
}
