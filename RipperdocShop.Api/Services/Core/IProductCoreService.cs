using RipperdocShop.Api.Models.Entities;

namespace RipperdocShop.Api.Services.Core;

public interface IProductCoreService
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product?> GetByIdWithDetailsAsync(Guid id);
}
