using Microsoft.EntityFrameworkCore;
using RipperdocShop.Data;
using RipperdocShop.Models.DTOs;
using RipperdocShop.Models.Entities;
using RipperdocShop.Services.Core;

namespace RipperdocShop.Services.Admin;

public class AdminCategoryService(
    ApplicationDbContext context,
    ICategoryCoreService categoryCoreService)
    : IAdminCategoryService
{
    public async Task<IEnumerable<Category>> GetAllAsync(bool includeDeleted)
    {
        return await context.Categories
            .Where(c => includeDeleted || c.DeletedAt == null)
            .ToListAsync();
    }
    
    public async Task<Category> CreateAsync(CategoryDto dto)
    {
        var category = new Category(dto.Name, dto.Description);
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> UpdateAsync(Guid id, CategoryDto dto)
    {
        var category = await categoryCoreService.GetByIdAsync(id);
        if (category == null)
            return null;
        
        if (category.DeletedAt != null)
            throw new InvalidOperationException("Cannot update a deleted category. Restore it first, choom.");

        category.UpdateDetails(dto.Name, dto.Description);
        await context.SaveChangesAsync();
        return category;
    }
    
    public async Task<Category?> SoftDeleteAsync(Guid id)
    {
        var category = await categoryCoreService.GetByIdAsync(id);
        if (category == null)
            return null;

        category.SoftDelete();
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> RestoreAsync(Guid id)
    {
        var category = await categoryCoreService.GetByIdAsync(id);
        if (category == null)
            return null;

        category.Restore();
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> DeletePermanentlyAsync(Guid id)
    {
        var category = await context.Categories
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(c => c.Id == id);
    
        if (category == null)
            return null;

        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        return category;
    }
}
