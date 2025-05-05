using RipperdocShop.Api.Services.Core;
using RipperdocShop.Shared.DTOs;
using AutoMapper;

namespace RipperdocShop.Api.Services.Customer;

public class CustomerBrandService(IBrandCoreService brandCoreService, IMapper mapper) : ICustomerBrandService
{
    public async Task<(IEnumerable<BrandResponseDto> Categories, int TotalCount, int TotalPages)> GetAllAsync(
        bool includeDeleted, int page, int pageSize)
    {
        var (brands, totalCount, totalPages) = await brandCoreService.GetAllAsync(includeDeleted, page, pageSize);
        var brandsDto = mapper.Map<IEnumerable<BrandResponseDto>>(brands);
        return (brandsDto, totalCount, totalPages);
    }
}
