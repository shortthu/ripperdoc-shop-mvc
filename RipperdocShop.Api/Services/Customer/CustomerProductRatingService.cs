using AutoMapper;
using RipperdocShop.Api.Data;
using RipperdocShop.Api.Models.Entities;
using RipperdocShop.Api.Services.Core;
using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Api.Services.Customer;

public class CustomerProductRatingService(
    IProductRatingCoreService ratingCoreService,
    IProductCoreService productCoreService,
    IUserService userService,
    ApplicationDbContext context,
    IMapper mapper)
    : ICustomerProductRatingService
{
    public async Task<ProductRating?> CreateAsync(ProductRatingDto createDto)
    {
        var product = await productCoreService.GetBySlugWithDetailsAsync(createDto.ProductSlug);
        if (product is not { DeletedAt: null }) return null;

        var user = await userService.GetByIdAsync(createDto.CustomerId);
        if (user is not { DeletedAt: null }) return null;

        var rating = new ProductRating(createDto.Score, createDto.Comment, product, user);
        context.ProductRatings.Add(rating);
        await context.SaveChangesAsync();
        return rating;
    }

    public async Task<ProductRatingResponseDto> GetByProductSlugAsync(string slug, bool includeDeleted, int page,
        int pageSize)
    {
        var (ratings, totalCount, totalPages) = await
            ratingCoreService.GetByProductSlugAsync(slug, includeDeleted, page, pageSize);
        var ratingsDto = mapper.Map<IEnumerable<ProductRatingDto>>(ratings);
        return new ProductRatingResponseDto()
        {
            Ratings = ratingsDto,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }

    public async Task<ProductRating?> UpdateAsync(Guid id, ProductRatingDto createDto)
    {
        var rating = await ratingCoreService.GetByIdAsync(id);
        if (rating == null)
            return null;

        if (rating.DeletedAt != null)
            throw new InvalidOperationException("Cannot update a deleted rating.");

        var product = await productCoreService.GetBySlugWithDetailsAsync(createDto.ProductSlug);
        if (product is not { DeletedAt: null }) return null;

        var user = await userService.GetByIdAsync(createDto.CustomerId);
        if (user is not { DeletedAt: null }) return null;

        rating.UpdateDetails(createDto.Score, createDto.Comment, product, user);
        await context.SaveChangesAsync();
        return rating;
    }
}
