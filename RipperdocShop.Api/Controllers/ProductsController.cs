using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RipperdocShop.Api.Models.DTOs;
using RipperdocShop.Api.Services.Core;
using RipperdocShop.Api.Services.Customer;
using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(ICustomerProductService productService,
    IProductCoreService productCoreService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var (products, totalCount, totalPages) = await productService.GetAllAsync(includeDeleted, page, pageSize);
        var response = new ProductResponse()
        {
            Products = products,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
        return Ok(response);
    }
    
    [HttpGet("slug")]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var product = await productService.GetBySlugAsync(slug);
        return product == null
            ? NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Product not found",
                Detail = $"Product with slug {slug} does not exist"
            })
            : Ok(product);
    }
    
    [HttpGet("featured")]
    public async Task<IActionResult> GetFeatured()
    {
        var products = await productService.GetFeaturedProductsAsync();
        return products.IsNullOrEmpty()
            ? NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "No featured product",
                Detail = "No featured product"
            })
            : Ok(products);
    }
}
