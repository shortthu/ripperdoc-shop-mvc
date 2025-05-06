using AutoMapper;
using RipperdocShop.Api.Services.Core;
using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Services.Customer;

public class CustomerProductService(IProductCoreService productCoreService, IMapper mapper) : ICustomerProductService
{
    public async Task<ProductResponseDto> GetAllAsync(
        bool includeDeleted, int page, int pageSize)
    {
        var (products, totalCount, totalPages) = await productCoreService.GetAllAsync(includeDeleted, page, pageSize);
        var productDto = mapper.Map<IEnumerable<ProductDto>>(products);
        return new ProductResponseDto()
        {
            Products = productDto,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }

    public async Task<ProductResponseDto>
        GetByCategorySlugAsync(string slug, bool includeDeleted, int page, int pageSize)
    {
        var (products, totalCount, totalPages) =
            await productCoreService.GetByCategorySlugAsync(slug, includeDeleted, page, pageSize);
        var productDto = mapper.Map<IEnumerable<ProductDto>>(products);
        return new ProductResponseDto()
        {
            Products = productDto,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }

    public async Task<ProductResponseDto>
        GetByBrandSlugAsync(string slug, bool includeDeleted, int page, int pageSize)
    {
        var (products, totalCount, totalPages) =
            await productCoreService.GetByBrandSlugAsync(slug, includeDeleted, page, pageSize);
        var productDto = mapper.Map<IEnumerable<ProductDto>>(products);
        return new ProductResponseDto()
        {
            Products = productDto,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }

    public async Task<ProductDto?> GetBySlugAsync(string slug)
    {
        var product = await productCoreService.GetBySlugWithDetailsAsync(slug);
        var productDto = mapper.Map<ProductDto>(product);
        return productDto;
    }

    public async Task<IEnumerable<ProductDto>> GetFeaturedProductsAsync()
    {
        var products = await productCoreService.GetFeaturedAsync();
        var productDto = mapper.Map<IEnumerable<ProductDto>>(products);
        return productDto;
    }
}
