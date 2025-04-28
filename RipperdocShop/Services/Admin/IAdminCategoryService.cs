using RipperdocShop.Models.DTOs;
using RipperdocShop.Models.Entities;

namespace RipperdocShop.Services.Admin;

public interface IAdminCategoryService
{
    Task<IEnumerable<Category>> GetAllAsync(bool includeDeleted);
    Task<Category> CreateAsync(CategoryDto dto);
    Task<Category?> UpdateAsync(Guid id, CategoryDto dto);
    Task<Category?> SoftDeleteAsync(Guid id);
    Task<Category?> RestoreAsync(Guid id);
    Task<Category?> DeletePermanentlyAsync(Guid id);
}
