using RipperdocShop.Api.Models.Identities;
using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Services.Admin;

public interface IAdminCustomerListService
{
    Task<(IEnumerable<UserDto> Users, int TotalCount, int TotalPages)> GetAllAsync(bool includeDeleted,
        int page, int pageSize);
}
