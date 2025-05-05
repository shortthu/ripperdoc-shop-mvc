using Microsoft.AspNetCore.Mvc;
using RipperdocShop.Api.Services.Core;
using RipperdocShop.Api.Services.Customer;
using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriesController(ICustomerCategoryService categoryService, ICategoryCoreService categoryCoreService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var (categories, totalCount, totalPages) = await categoryService.GetAllAsync(includeDeleted, page, pageSize);
        var response = new CategoryResponse()
        {
            Categories = categories,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
        return Ok(response);
    }
}
