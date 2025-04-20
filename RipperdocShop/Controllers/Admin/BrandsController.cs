using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RipperdocShop.Data;
using RipperdocShop.Models.Entities;
using RipperdocShop.Models.DTOs;

namespace RipperdocShop.Controllers.Admin;

[Route("api/admin/brands")]
[ApiController]
[Authorize(Roles = "Admin")]
public class BrandsController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false)
    {
        var brands = await context.Brands
            .Where(b => includeDeleted || b.DeletedAt == null)
            .ToListAsync();

        return Ok(brands);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(BrandDto dto)
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
        return brand == null ? NotFound() : Ok(brand);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, BrandDto dto)
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
