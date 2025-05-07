using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Models.ViewModels;

public class HomeViewModel
{
    public PaginatedCategoryResponse? Categories { get; set; }
    public List<ProductDto>? FeaturedProducts { get; set; } = [];
}
