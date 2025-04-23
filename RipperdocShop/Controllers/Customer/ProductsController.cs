using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RipperdocShop.Data;
using RipperdocShop.Models.Entities;

namespace RipperdocShop.Controllers.Customer;

[ApiController]
[Route("api/user/products")]
public class ProductsController(ApplicationDbContext db) : Controller
{
    [HttpGet("{slug}")]
    public async Task<ActionResult<Product>> GetBySlug(string slug)
    {
        var product = await db.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Slug == slug && p.DeletedAt != null);

        if (product is null) return NotFound();
        
        return View(product);
    }
}
