using Microsoft.AspNetCore.Identity;
using RipperdocShop.Models.Identities;

namespace RipperdocShop.Data;

public static class IdentitySeeder
{
    
    public static async Task SeedAsync(IServiceProvider services, string adminEmail, string adminPassword)
    {
        using var scope = services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        
        // Seed Roles
        
        string[] roleNames = ["Admin", "Customer"];
        
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper(),
                });
            }
        }
        
        // Seed Admin User

        // if (!await roleManager.RoleExistsAsync("Admin"))
        // {
        //     await roleManager.CreateAsync(new IdentityRole("Admin"));
        // }

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            var user = new AppUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(user, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create admin user: {errors}");
            }
        }
    }

}
