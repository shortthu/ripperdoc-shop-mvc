using RipperdocShop.Shared.DTOs;
using RipperdocShop.Api.Models.Entities;

namespace RipperdocShop.Api.Services.Admin;

public interface IAdminProductService
{
    Task<(IEnumerable<Product> Products, int TotalCount, int TotalPages)> GetAllAsync(
        bool includeDeleted, int page, int pageSize);
    Task<Product> CreateAsync(ProductDto dto, Category category, Brand? brand);
    Task<Product?> UpdateAsync(Guid id, ProductDto dto, Category category, Brand? brand);
    Task<Product?> SetFeaturedAsync(Guid id);
    Task<Product?> RemoveFeaturedAsync(Guid id);
    Task<Product?> SoftDeleteAsync(Guid id);
    Task<Product?> RestoreAsync(Guid id);
    Task<Product?> DeletePermanentlyAsync(Guid id);
}
