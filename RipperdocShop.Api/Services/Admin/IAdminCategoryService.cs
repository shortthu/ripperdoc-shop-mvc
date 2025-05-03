using RipperdocShop.Shared.DTOs;
using RipperdocShop.Api.Models.Entities;

namespace RipperdocShop.Api.Services.Admin;

public interface IAdminCategoryService
{
    Task<(IEnumerable<Category> Categories, int TotalCount, int TotalPages)> GetAllAsync(
        bool includeDeleted, int page, int pageSize);
    Task<Category> CreateAsync(CategoryDto dto);
    Task<Category?> UpdateAsync(Guid id, CategoryDto dto);
    Task<Category?> SoftDeleteAsync(Guid id);
    Task<Category?> RestoreAsync(Guid id);
    Task<Category?> DeletePermanentlyAsync(Guid id);
}
