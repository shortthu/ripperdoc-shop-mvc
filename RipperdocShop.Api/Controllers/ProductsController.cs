using Microsoft.AspNetCore.Mvc;
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
}
