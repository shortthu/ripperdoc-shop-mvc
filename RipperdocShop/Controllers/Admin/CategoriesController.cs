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
public class CategoriesController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false)
    {
        var query = context.Categories.AsQueryable();
        
        if (!includeDeleted)
            query = query.Where(c => c.DeletedAt == null);
        
        var categories = await query
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

        return Ok(categories);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto dto)
    {
        var category = new Category(dto.Name, dto.Description);
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return NotFound();

        return Ok(new {
            category.Id,
            category.Name,
            category.Slug,
            category.Description,
            category.CreatedAt,
            category.UpdatedAt,
            category.DeletedAt
        });
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, CategoryDto dto)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        
        if (category.DeletedAt != null)
            return BadRequest("Cannot update a deleted category. Restore it first, choom.");

        category.UpdateDetails(dto.Name, dto.Description);
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> SoftDelete(Guid id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return NotFound();

        category.SoftDelete();
        await context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpPost("{id:guid}/restore")]
    public async Task<IActionResult> Restore(Guid id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return NotFound();

        category.Restore();
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}/hard")]
    public async Task<IActionResult> HardDelete(Guid id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return NotFound();

        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return NoContent();
    }
}