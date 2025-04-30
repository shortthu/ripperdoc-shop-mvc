using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RipperdocShop.Shared.DTOs;
using RipperdocShop.Api.Services.Admin;
using RipperdocShop.Api.Services.Core;

namespace RipperdocShop.Api.Controllers.Admin;

[Route("api/admin/categories")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
public class CategoriesController(
    IAdminCategoryService categoryService,
    ICategoryCoreService categoryCoreService)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false)
    {
        var categories = await categoryService.GetAllAsync(includeDeleted);
        return Ok(categories);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CategoryDto dto)
    {
        try
        {
            var category = await categoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Could not create category",
                Detail = e.Message
            });
        }
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await categoryCoreService.GetByIdAsync(id);
        
        if (category == null)
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Category not found",
                Detail = $"Category with ID {id} does not exist"
            });
            
        return Ok(category);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, CategoryDto dto)
    {
        try
        {
            var category = await categoryService.UpdateAsync(id, dto);
            
            if (category == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = $"Category with ID {id} does not exist"
                });
                
            return NoContent();
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid operation",
                Detail = e.Message
            });
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SoftDelete(Guid id)
    {
        try
        {
            var category = await categoryService.SoftDeleteAsync(id);
            
            if (category == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = $"Category with ID {id} does not exist"
                });
                
            return NoContent();
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid operation",
                Detail = e.Message
            });
        }
    }

    [HttpPost("{id:guid}/restore")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Restore(Guid id)
    {
        try
        {
            var category = await categoryService.RestoreAsync(id);
            
            if (category == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = $"Category with ID {id} does not exist"
                });
                
            return NoContent();
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid operation",
                Detail = e.Message
            });
        }
    }

    [HttpDelete("{id:guid}/hard")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePermanently(Guid id)
    {
        try
        {
            var category = await categoryService.DeletePermanentlyAsync(id);
            
            if (category == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = $"Category with ID {id} does not exist"
                });
                
            return NoContent();
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid operation",
                Detail = e.Message
            });
        }
    }
}
