using RipperdocShop.Api.Models.Identities;

namespace RipperdocShop.Api.Services.Core;

public interface IUserService
{
    Task<AppUser?> GetByIdAsync(Guid id);
}
