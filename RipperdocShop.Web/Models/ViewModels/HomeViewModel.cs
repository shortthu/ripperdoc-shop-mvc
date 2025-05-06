using RipperdocShop.Shared.DTOs;

namespace RipperdocShop.Web.Models.ViewModels;

public class HomeViewModel
{
    public CategoryResponseDto? Categories { get; set; }
    public List<ProductDto>? FeaturedProducts { get; set; } = [];
}
