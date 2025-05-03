using Microsoft.EntityFrameworkCore;
using RipperdocShop.Api.Data;
using RipperdocShop.Api.Models.Entities;

namespace RipperdocShop.Api.Services.Core;

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

    public async Task<(IEnumerable<Product> Products, int TotalCount, int TotalPages)> GetAllAsync(
        bool includeDeleted, int page, int pageSize)
    {
        var query = context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Where(p => includeDeleted || p.DeletedAt == null);

        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var products = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (products, totalCount, totalPages);
    }
}
