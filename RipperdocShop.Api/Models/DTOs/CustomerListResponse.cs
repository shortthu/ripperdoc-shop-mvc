using RipperdocShop.Api.Models.Identities;
using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Models.DTOs;

public class CustomerListResponse
{
    public IEnumerable<UserDto> Customers { get; init; } = null!;
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
}
