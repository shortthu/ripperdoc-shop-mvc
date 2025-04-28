using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RipperdocShop.Data;
using RipperdocShop.Models.Entities;
using RipperdocShop.Models.DTOs;
using RipperdocShop.Services.Admin;
using RipperdocShop.Services.Core;

namespace RipperdocShop.Controllers.Admin;

[Route("api/admin/brands")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

public class BrandsController(
    ApplicationDbContext context, 
    IAdminBrandService adminBrandService, 
    IBrandCoreService brandCoreService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false)
    {
        var brands = await adminBrandService.GetAllAsync(includeDeleted);

        return Ok(brands);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(BrandDto dto)
    {
        try
        {
            var brand = await adminBrandService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = brand.Id }, brand);
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var brand = await brandCoreService.GetByIdAsync(id);
        return brand == null ? NotFound() : Ok(brand);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, BrandDto dto)
    {
        try
        {
            var brand = await adminBrandService.UpdateAsync(id, dto);
            if (brand == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = $"Brand with ID {id} does not exist"
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
            var brand = await adminBrandService.SoftDeleteAsync(id);
            if (brand == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = $"Brand with ID {id} does not exist"
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
            var brand = await adminBrandService.RestoreAsync(id);
            if (brand == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = $"Brand with ID {id} does not exist"
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePermanently(Guid id)
    {
        try
        {
            var brand = await adminBrandService.DeletePermanentlyAsync(id);
            if (brand == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = $"Brand with ID {id} does not exist"
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
