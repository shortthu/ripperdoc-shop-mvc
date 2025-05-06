using RipperdocShop.Api.Models.Entities;
using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Services.Customer;

public interface ICustomerProductRatingService
{
    Task<ProductRating> CreateAsync(ProductRatingDto createDto);

    Task<ProductRating?> UpdateAsync(Guid id, ProductRatingDto createDto);
}
