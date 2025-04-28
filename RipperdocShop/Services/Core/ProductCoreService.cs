using Microsoft.EntityFrameworkCore;
using RipperdocShop.Data;
using RipperdocShop.Models.Entities;

namespace RipperdocShop.Services.Core;

public class ProductCoreService(ApplicationDbContext context) : IProductCoreService
{
    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await context.Products.FindAsync(id);
    }
    
    public async Task<Product?> GetByIdWithDetailsAsync(Guid id)
    {
        return await context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
