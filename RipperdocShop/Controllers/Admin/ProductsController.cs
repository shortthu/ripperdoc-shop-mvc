using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RipperdocShop.Data;
using RipperdocShop.Models.DTOs;
using RipperdocShop.Models.Entities;
using RipperdocShop.Services.Admin;
using RipperdocShop.Services.Core;

namespace RipperdocShop.Controllers.Admin;

[Route("api/admin/products")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
public class ProductsController(
    ApplicationDbContext context,
    IAdminProductService adminProductService, 
    IProductCoreService productCoreService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false, 
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var (products, totalCount, totalPages) = await adminProductService.GetAllAsync(includeDeleted, page, pageSize);
        var response = new ProductResponse()
        {
            Products = products,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
        return Ok(response);
        
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await productCoreService.GetByIdAsync(id);
        return product == null ? NotFound(new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Product not found",
            Detail = $"Product with ID {id} does not exist"
        }) : Ok(product);
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

        var product = await adminProductService.CreateAsync(dto, category, brand);;

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ProductDto dto)
    {
        var category = await context.Categories.FindAsync(dto.CategoryId);
        if (category == null) return BadRequest("Category not found");

        Brand? brand = null;
        if (dto.BrandId != null)
        {
            brand = await context.Brands.FindAsync(dto.BrandId);
            if (brand == null) return BadRequest("Brand not found");
        }

        try
        {
            var product = await adminProductService.UpdateAsync(id, dto, category, brand);
            if (product == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = $"Product with ID {id} does not exist"
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
    
    [HttpPost("{id:guid}/feature")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SetFeatured(Guid id)
    {
        try
        {
            var product = await adminProductService.SetFeaturedAsync(id);
            if (product == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = $"Product with ID {id} does not exist"
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
    
    [HttpPost("{id:guid}/unfeature")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveFeatured(Guid id)
    {
        try
        {
            var product = await adminProductService.RemoveFeaturedAsync(id);
            if (product == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = $"Product with ID {id} does not exist"
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
            var product = await adminProductService.SoftDeleteAsync(id);
            if (product == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = $"Product with ID {id} does not exist"
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
            var product = await adminProductService.RestoreAsync(id);
            if (product == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = $"Product with ID {id} does not exist"
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
            var product = await adminProductService.DeletePermanentlyAsync(id);
            if (product == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = $"Product with ID {id} does not exist"
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
