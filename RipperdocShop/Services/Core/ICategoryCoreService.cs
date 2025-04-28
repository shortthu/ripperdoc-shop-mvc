using RipperdocShop.Models.Entities;

namespace RipperdocShop.Services.Core;

public interface ICategoryCoreService
{
    Task<Category?> GetByIdAsync(Guid id);
}
