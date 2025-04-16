using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RipperdocShop.Data;
using RipperdocShop.Models.Entities;

namespace RipperdocShop.Controllers.Products;

[ApiController]
[Route("api/user/products")]
public class UserProductsController(ApplicationDbContext db) : ControllerBase
{
    [HttpGet("{slug}")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetBySlug(string slug)
    {
        var product = await db.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Slug == slug && p.DeletedAt != null);

        if (product is null) return NotFound();
        
        return Ok(product);
    }
}