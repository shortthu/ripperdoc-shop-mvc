using RipperdocShop.Api.Models.Entities;

namespace RipperdocShop.Api.Services.Admin;

public interface IAdminProductRatingService
{
    Task<IEnumerable<ProductRating>> GetRecentAsync(int count, bool includeDeleted);
    Task<ProductRating?> SoftDeleteAsync(Guid id);
    Task<ProductRating?> RestoreAsync(Guid id);
    Task<ProductRating?> DeletePermanentlyAsync(Guid id);
}
