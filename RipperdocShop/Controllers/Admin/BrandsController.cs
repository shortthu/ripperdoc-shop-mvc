using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RipperdocShop.Data;
using RipperdocShop.Models.Entities;
using RipperdocShop.Models.DTOs;

namespace RipperdocShop.Controllers.Admin;

[Route("api/admin/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class BrandsController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false)
    {
        var query = context.Brands.AsQueryable();
        
        if (!includeDeleted)
            query = query.Where(c => c.DeletedAt == null);
        
        var brands = await query
            .Select(c => new {
                c.Id,
                c.Name,
                c.Slug,
                c.Description,
                c.CreatedAt,
                c.UpdatedAt,
                c.DeletedAt
            })
            .ToListAsync();

        return Ok(brands);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto dto)
    {
        var brand = new Brand(dto.Name, dto.Description);
        context.Brands.Add(brand);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = brand.Id }, brand);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var brand = await context.Brands.FindAsync(id);
        if (brand == null) return NotFound();

        return Ok(new {
            brand.Id,
            brand.Name,
            brand.Slug,
            brand.Description,
            brand.CreatedAt,
            brand.UpdatedAt,
            brand.DeletedAt
        });
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, CategoryDto dto)
    {
        var brand = await context.Brands.FindAsync(id);
        if (brand == null) return NotFound();
        
        if (brand.DeletedAt != null)
            return BadRequest("Cannot update a deleted brand. Restore it first, choom.");

        brand.UpdateDetails(dto.Name, dto.Description);
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> SoftDelete(Guid id)
    {
        var brand = await context.Brands.FindAsync(id);
        if (brand == null) return NotFound();

        brand.SoftDelete();
        await context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpPost("{id:guid}/restore")]
    public async Task<IActionResult> Restore(Guid id)
    {
        var brand = await context.Brands.FindAsync(id);
        if (brand == null) return NotFound();

        brand.Restore();
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}/hard")]
    public async Task<IActionResult> HardDelete(Guid id)
    {
        var brand = await context.Brands.FindAsync(id);
        if (brand == null) return NotFound();

        context.Brands.Remove(brand);
        await context.SaveChangesAsync();

        return NoContent();
    }
}