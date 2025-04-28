using Microsoft.EntityFrameworkCore;
using RipperdocShop.Data;
using RipperdocShop.Models.DTOs;
using RipperdocShop.Models.Entities;
using RipperdocShop.Services.Core;

namespace RipperdocShop.Services.Admin;

public class AdminBrandService(
    ApplicationDbContext context,
    IBrandCoreService brandCoreService)
    : IAdminBrandService
{
    public async Task<IEnumerable<Brand>> GetAllAsync(bool includeDeleted)
    {
        return await context.Brands
            .Where(b => includeDeleted || b.DeletedAt == null)
            .ToListAsync();
    }
    
    public async Task<Brand> CreateAsync(BrandDto dto)
    {
        var brand = new Brand(dto.Name, dto.Description);
        context.Brands.Add(brand);
        await context.SaveChangesAsync();
        return brand;
    }

    public async Task<Brand?> UpdateAsync(Guid id, BrandDto dto)
    {
        var brand = await brandCoreService.GetByIdAsync(id);
        if (brand == null)
            return null;
        
        if (brand.DeletedAt != null)
            throw new InvalidOperationException("Cannot update a deleted brand. Restore it first, choom.");

        brand.UpdateDetails(dto.Name, dto.Description);
        await context.SaveChangesAsync();
        return brand;
    }
    
    public async Task<Brand?> SoftDeleteAsync(Guid id)
    {
        var brand = await brandCoreService.GetByIdAsync(id);
        if (brand == null)
            return null;

        brand.SoftDelete();
        await context.SaveChangesAsync();
        return brand;
    }


    public async Task<Brand?> RestoreAsync(Guid id)
    {
        var brand = await brandCoreService.GetByIdAsync(id);
        if (brand == null)
            return null;

        brand.Restore();
        await context.SaveChangesAsync();
        return brand;
    }


    public async Task<Brand?> DeletePermanentlyAsync(Guid id)
    {
        var brand = await context.Brands
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == id);
    
        if (brand == null)
            return null;

        context.Brands.Remove(brand);
        await context.SaveChangesAsync();
        return brand;
    }
}
