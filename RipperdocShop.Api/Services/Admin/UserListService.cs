using Microsoft.EntityFrameworkCore;
using RipperdocShop.Api.Data;
using RipperdocShop.Api.Models.Identities;
using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Services.Admin;

public class UserListService(ApplicationDbContext context) : IUserListService
{
    public async Task<(IEnumerable<UserDto> Users, int TotalCount, int TotalPages)> GetAllAsync(bool includeDeleted,
        int page, int pageSize)
    {
        var query = context.Users
            .Where(u => includeDeleted || u.DeletedAt == null)
            .Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName!,
                Email = u.Email!,
                EmailConfirmed = u.EmailConfirmed,
                LockoutEnabled = u.LockoutEnabled,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                DeletedAt = u.DeletedAt,
                IsDisabled = u.IsDisabled,
                Roles = (from ur in context.UserRoles
                    join r in context.Roles on ur.RoleId equals r.Id
                    where ur.UserId == u.Id
                    select r.Name).ToList()
            });

        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var users = await query
            .OrderByDescending(u => u.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (users, totalCount, totalPages);
    }
}
