using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RipperdocShop.Data;
using RipperdocShop.Models.DTOs;
using RipperdocShop.Models.Entities;

namespace RipperdocShop.Controllers.Admin;

[Route("api/admin/products")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
public class ProductsController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false, 
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Where(p => includeDeleted || p.DeletedAt == null);
        
        var totalItems = await query.CountAsync();
        
        var products = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new { totalItems, products });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id);

        return product == null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDto dto)
    {
        var category = await context.Categories.FindAsync(dto.CategoryId);
        if (category == null) return BadRequest("Category not found");

        Brand? brand = null;
        if (dto.BrandId != null)
        {
            brand = await context.Brands.FindAsync(dto.BrandId);
            if (brand == null) return BadRequest("Brand not found");
        }

        var product = new Product(dto.Name, dto.Description, dto.ImageUrl, dto.Price, category, dto.IsFeatured, brand);

        context.Products.Add(product);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ProductDto dto)
    {
        var product = await context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return NotFound();
        if (product.DeletedAt != null) return BadRequest("Cannot update a deleted product");

        var category = await context.Categories.FindAsync(dto.CategoryId);
        if (category == null) return BadRequest("Category not found");

        Brand? brand = null;
        if (dto.BrandId != null)
        {
            brand = await context.Brands.FindAsync(dto.BrandId);
            if (brand == null) return BadRequest("Brand not found");
        }

        product.UpdateDetails(dto.Name, dto.Description, dto.ImageUrl, dto.Price, category, brand);

        await context.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpPost("{id:guid}/feature")]
    public async Task<IActionResult> SetFeatured(Guid id)
    {
        var product = await context.Products.FindAsync(id);
        if (product is not { DeletedAt: null })
            return NotFound();

        try
        {
            product.SetFeatured();
            await context.SaveChangesAsync();
            return NoContent();
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(new { error = e.Message });
        }
    }

    [HttpPost("{id:guid}/unfeature")]
    public async Task<IActionResult> RemoveFeatured(Guid id)
    {
        var product = await context.Products.FindAsync(id);
        if (product is not { DeletedAt: null })
            return NotFound();

        try
        {
            product.RemoveFeatured();
            await context.SaveChangesAsync();
            return NoContent();
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(new { error = e.Message });
        }
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> SoftDelete(Guid id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null) return NotFound();

        try
        {
            product.SoftDelete();
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
        var product = await context.Products.FindAsync(id);
        if (product == null) return NotFound();

        try
        {
            product.Restore();
            await context.SaveChangesAsync();
            return NoContent();
        } catch (InvalidOperationException e)
        {
            return BadRequest(new { error = e.Message });
        }
        
    }

    [HttpDelete("{id:guid}/hard")]
    public async Task<IActionResult> DeletePermanently(Guid id)
    {
        var product = await context.Products
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return NotFound();

        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
