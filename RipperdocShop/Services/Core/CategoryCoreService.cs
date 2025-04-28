using RipperdocShop.Data;
using RipperdocShop.Models.Entities;

namespace RipperdocShop.Services.Core;

public class CategoryCoreService(ApplicationDbContext context) : ICategoryCoreService
{
    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await context.Categories.FindAsync(id);
    }
}
