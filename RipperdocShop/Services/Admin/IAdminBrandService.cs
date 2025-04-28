using RipperdocShop.Models.DTOs;
using RipperdocShop.Models.Entities;

namespace RipperdocShop.Services.Admin;

public interface IAdminBrandService
{
    Task<IEnumerable<Brand>> GetAllAsync(bool includeDeleted);
    Task<Brand> CreateAsync(BrandDto dto);
    Task<Brand?> UpdateAsync(Guid id, BrandDto dto);
    Task<Brand?> SoftDeleteAsync(Guid id);
    Task<Brand?> RestoreAsync(Guid id);
    Task<Brand?> DeletePermanentlyAsync(Guid id);
}

