using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RipperdocShop.Api.Models.Entities;
using RipperdocShop.Api.Services.Core;
using RipperdocShop.Api.Services.Customer;
using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Controllers.Customer;

[Route("ratings")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Customer")]
public class ProductRatingController(
    ICustomerProductRatingService productRatingService,
    ProductRatingCoreService coreService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(ProductRatingDto createDto)
    {
        try
        {
            var rating = await productRatingService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = rating?.Id }, rating);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Could not create rating",
                Detail = e.Message
            });
        }
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var rating = await coreService.GetByIdAsync(id);
        return rating == null
            ? NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Rating not found",
                Detail = $"Rating with ID {id} does not exist"
            })
            : Ok(rating);
    }
    
    [HttpGet("by-product/{slug}")]
    public async Task<IActionResult> GetByProductSlug(string slug, [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var response = await productRatingService.GetByProductSlugAsync(slug, false, page, pageSize);
        return Ok(response);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, ProductRatingDto createDto)
    {
        try
        {
            var rating = await productRatingService.UpdateAsync(id, createDto);
            if (rating == null)
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = $"Rating with ID {id} does not exist"
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
