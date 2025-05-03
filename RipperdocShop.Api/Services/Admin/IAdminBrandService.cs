using RipperdocShop.Shared.DTOs;
using RipperdocShop.Api.Models.Entities;

namespace RipperdocShop.Api.Services.Admin;

public interface IAdminBrandService
{
    Task<(IEnumerable<Brand> Brands, int TotalCount, int TotalPages)> GetAllAsync(bool includeDeleted,
        int page, int pageSize);
    Task<Brand> CreateAsync(BrandDto dto);
    Task<Brand?> UpdateAsync(Guid id, BrandDto dto);
    Task<Brand?> SoftDeleteAsync(Guid id);
    Task<Brand?> RestoreAsync(Guid id);
    Task<Brand?> DeletePermanentlyAsync(Guid id);
}

