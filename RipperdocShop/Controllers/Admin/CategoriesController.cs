using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RipperdocShop.Data;
using RipperdocShop.Models.Entities;
using RipperdocShop.Models.DTOs;

namespace RipperdocShop.Controllers.Admin;

[Route("api/admin/categories")]
[ApiController]
[Authorize(Roles = "Admin")]
public class CategoriesController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false)
    {
        var categories = await context.Categories
            .Where(c => includeDeleted || c.DeletedAt == null)
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
        return category == null ? NotFound() : Ok(category);
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

        try
        {
            category.SoftDelete();
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
        var category = await context.Categories.FindAsync(id);
        if (category == null) return NotFound();

        try
        {
            category.Restore();
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
        var category = await context.Categories
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == id);
        if (category == null) return NotFound();

        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
