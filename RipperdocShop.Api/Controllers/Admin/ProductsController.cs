using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RipperdocShop.Api.Data;
using RipperdocShop.Api.Models.DTOs;
using RipperdocShop.Shared.DTOs;
using RipperdocShop.Api.Models.Entities;
using RipperdocShop.Api.Services.Admin;
using RipperdocShop.Api.Services.Core;

namespace RipperdocShop.Api.Controllers.Admin;

[Route("api/admin/products")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
public class ProductsController(
    ApplicationDbContext context,
    IAdminProductService productService,
    IProductCoreService productCoreService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var (products, totalCount, totalPages) = await productCoreService.GetAllAsync(includeDeleted, page, pageSize);
        var response = new ProductAdminResponse()
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
        return product == null
            ? NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Product not found",
                Detail = $"Product with ID {id} does not exist"
            })
            : Ok(product);
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

        var product = await productService.CreateAsync(dto, category, brand);

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
            var product = await productService.UpdateAsync(id, dto, category, brand);
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
            var product = await productService.SetFeaturedAsync(id);
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
            var product = await productService.RemoveFeaturedAsync(id);
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
            var product = await productService.SoftDeleteAsync(id);
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
            var product = await productService.RestoreAsync(id);
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
            var product = await productService.DeletePermanentlyAsync(id);
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
