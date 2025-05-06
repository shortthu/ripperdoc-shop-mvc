using RipperdocShop.Api.Services.Core;
using RipperdocShop.Shared.DTOs;
using AutoMapper;

namespace RipperdocShop.Api.Services.Customer;

public class CustomerCategoryService(ICategoryCoreService categoryCoreService, IMapper mapper) : ICustomerCategoryService
{
    public async Task<(IEnumerable<CategoryDto> Categories, int TotalCount, int TotalPages)> GetAllAsync(
        bool includeDeleted, int page, int pageSize)
    {
        var (categories, totalCount, totalPages) = await categoryCoreService.GetAllAsync(includeDeleted, page, pageSize);
        var categoriesDto = mapper.Map<IEnumerable<CategoryDto>>(categories);
        return (categoriesDto, totalCount, totalPages);
    }
}
