using Microsoft.AspNetCore.Mvc;
using RipperdocShop.Api.Services.Core;
using RipperdocShop.Api.Services.Customer;
using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Controllers;

[Route("api/brands")]
[ApiController]
public class BrandsController(ICustomerBrandService brandService, IBrandCoreService brandCoreService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var (brands, totalCount, totalPages) = await brandService.GetAllAsync(includeDeleted, page, pageSize);
        var response = new BrandResponse()
        {
            Brands = brands,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
        return Ok(response);
    }
}
