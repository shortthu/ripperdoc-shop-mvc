using RipperdocShop.Api.Models.Entities;

namespace RipperdocShop.Api.Services.Core;

public interface IProductCoreService
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product?> GetByIdWithDetailsAsync(Guid id);

    Task<(IEnumerable<Product> Products, int TotalCount, int TotalPages)> GetAllAsync(
        bool includeDeleted, int page, int pageSize);
}
