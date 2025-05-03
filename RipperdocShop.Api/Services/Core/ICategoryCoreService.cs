using RipperdocShop.Api.Models.Entities;

namespace RipperdocShop.Api.Services.Core;

public interface ICategoryCoreService
{
    Task<Category?> GetByIdAsync(Guid id);

    Task<(IEnumerable<Category> Categories, int TotalCount, int TotalPages)> GetAllAsync(
        bool includeDeleted, int page, int pageSize);
}
