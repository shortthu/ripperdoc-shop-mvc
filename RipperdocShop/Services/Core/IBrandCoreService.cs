using RipperdocShop.Models.Entities;

namespace RipperdocShop.Services.Core;

public interface IBrandCoreService
{
    Task<Brand?> GetByIdAsync(Guid id);
    // Task<bool> ExistsAsync(Guid id);
    // Task<Brand?> GetByIdWithProductsAsync(Guid id);  // If you need to include related data
}
