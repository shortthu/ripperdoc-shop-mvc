using System.ComponentModel.DataAnnotations;

namespace RipperdocShop.Shared.ViewModels;

public class ProductRatingUpdateViewModel
{
    [Range(1, 5)]
    public int Score { get; set; }

    public string? Comment { get; set; }
}
