using RipperdocShop.Api.Data;
using RipperdocShop.Api.Models.Entities;

namespace RipperdocShop.Api.Services.Core;

public class CategoryCoreService(ApplicationDbContext context) : ICategoryCoreService
{
    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await context.Categories.FindAsync(id);
    }
}
