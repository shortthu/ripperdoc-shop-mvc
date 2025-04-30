using RipperdocShop.Api.Data;
using RipperdocShop.Api.Models.Entities;

namespace RipperdocShop.Api.Services.Core;

public class BrandCoreService(ApplicationDbContext context) : IBrandCoreService
{
    public async Task<Brand?> GetByIdAsync(Guid id)
    {
        return await context.Brands.FindAsync(id);
    }
}
