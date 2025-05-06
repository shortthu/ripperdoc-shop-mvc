using RipperdocShop.Api.Models.Entities;
using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Services.Customer;

public interface ICustomerProductRatingService
{
    Task<ProductRating?> CreateAsync(ProductRatingDto createDto);

    Task<ProductRatingResponseDto> GetByProductSlugAsync(string slug, bool includeDeleted, int page,
        int pageSize);

    Task<ProductRating?> UpdateAsync(Guid id, ProductRatingDto createDto);
}
