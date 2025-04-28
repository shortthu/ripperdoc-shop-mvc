using RipperdocShop.Models.Entities;

namespace RipperdocShop.Services.Core;

public interface IProductCoreService
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product?> GetByIdWithDetailsAsync(Guid id);
}
