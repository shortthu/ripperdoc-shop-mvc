using AutoMapper;
using RipperdocShop.Api.Services.Core;
using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Services.Customer;

public class CustomerProductService(IProductCoreService productCoreService, IMapper mapper) : ICustomerProductService
{
    public async Task<(IEnumerable<ProductResponseDto> Products, int TotalCount, int TotalPages)> GetAllAsync(
        bool includeDeleted, int page, int pageSize)
    {
        var (products, totalCount, totalPages) = await productCoreService.GetAllAsync(includeDeleted, page, pageSize);
        var productDto = mapper.Map<IEnumerable<ProductResponseDto>>(products);
        return (productDto, totalCount, totalPages);
    }
    
    public async Task<ProductResponseDto?> GetBySlugAsync(string slug)
    {
        var product = await productCoreService.GetBySlugWithDetailsAsync(slug);
        var productDto = mapper.Map<ProductResponseDto>(product);
        return productDto;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetFeaturedProductsAsync()
    {
        var products = await productCoreService.GetFeaturedAsync();
        var productDto = mapper.Map<IEnumerable<ProductResponseDto>>(products);
        return productDto;
    }
}
