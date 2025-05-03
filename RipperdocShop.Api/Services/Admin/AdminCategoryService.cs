using Microsoft.EntityFrameworkCore;
using RipperdocShop.Api.Data;
using RipperdocShop.Shared.DTOs;
using RipperdocShop.Api.Models.Entities;
using RipperdocShop.Api.Services.Core;

namespace RipperdocShop.Api.Services.Admin;

public class AdminCategoryService(
    ApplicationDbContext context,
    ICategoryCoreService categoryCoreService)
    : IAdminCategoryService
{
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
