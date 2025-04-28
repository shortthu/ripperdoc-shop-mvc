using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RipperdocShop.Models.DTOs;
using RipperdocShop.Models.Identities;
using RipperdocShop.Services;

namespace RipperdocShop.Controllers;

// This route is ONLY for the Admin user

[ApiController]
[Route("api/auth")]
public class AuthController(
    SignInManager<AppUser> signInManager,
    UserManager<AppUser> userManager,
    JwtService jwt)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user is not { DeletedAt: null } || user.IsDisabled)
            return Unauthorized("User doesn't exist or access revoked");

        var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded)
            return Unauthorized("Wrong creds, choom");

        var roles = await userManager.GetRolesAsync(user);
        if (!roles.Contains("Admin"))
            return Unauthorized("Looking at the wrong place, choom");
        
        var token = jwt.GenerateToken(user, roles);
        
        Response.Cookies.Append("AdminAccessToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(2)
        });

        return Ok(new { message = "Access granted" });
    }
}
