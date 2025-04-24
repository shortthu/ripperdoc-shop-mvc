using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RipperdocShop.Data;

namespace RipperdocShop.Controllers.Admin;

[Route("api/admin/ratings")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
public class ProductRatingsController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet("by-product/{productId:guid}")]
    public async Task<IActionResult> GetByProduct(Guid productId, [FromQuery] bool includeDeleted = false)
    {
        var ratings = await context.ProductRatings
            .Where(r => r.ProductId == productId)
            .Where(r => includeDeleted || r.DeletedAt == null)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new
            {
                r.Id,
                r.Score,
                r.Comment,
                r.CreatedAt,
                r.UpdatedAt,
                r.UserId,
                Username = r.User.UserName
            })
            .ToListAsync();

        return Ok(ratings);
    }

    [HttpGet("by-user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(Guid userId, [FromQuery] bool includeDeleted = false)
    {
        var ratings = await context.ProductRatings
            .Where(r => r.UserId == userId)
            .Where(r => includeDeleted || r.DeletedAt == null)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new
            {
                r.Id,
                r.Score,
                r.Comment,
                r.CreatedAt,
                r.UpdatedAt,
                r.ProductId,
                ProductName = r.Product.Name
            })
            .ToListAsync();

        return Ok(ratings);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> SoftDelete(Guid id)
    {
        var ratings = await context.ProductRatings.FindAsync(id);
        if (ratings == null) return NotFound();

        try
        {
            ratings.SoftDelete();
            await context.SaveChangesAsync();
            return NoContent();
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(new { error = e.Message });
        }
    }

    [HttpPost("{id:guid}/restore")]
    public async Task<IActionResult> Restore(Guid id)
    {
        var ratings = await context.ProductRatings.FindAsync(id);
        if (ratings == null) return NotFound();

        try
        {
            ratings.Restore();
            await context.SaveChangesAsync();
            return NoContent();
        } catch (InvalidOperationException e)
        {
            return BadRequest(new { error = e.Message });
        }
    }
}
