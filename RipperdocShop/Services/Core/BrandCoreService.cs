using RipperdocShop.Data;
using RipperdocShop.Models.Entities;

namespace RipperdocShop.Services.Core;

public class BrandCoreService(ApplicationDbContext context) : IBrandCoreService
{
    public async Task<Brand?> GetByIdAsync(Guid id)
    {
        return await context.Brands.FindAsync(id);
    }
}
