using RipperdocShop.Api.Models.Entities;

namespace RipperdocShop.Api.Services.Core;

public interface ICategoryCoreService
{
    Task<Category?> GetByIdAsync(Guid id);
}
