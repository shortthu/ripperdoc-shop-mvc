using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Models.ViewModels;

public class ProductDetailsViewModel
{
    public List<ProductRatingDto>? Ratings { get; init; }
    public ProductDto? Product { get; init; }
}
