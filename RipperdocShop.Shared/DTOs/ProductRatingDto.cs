namespace RipperdocShop.Shared.DTOs;

public class ProductRatingDto
{
    public int Score { get; set; }

    public string? Comment { get; set; }

    public string ProductSlug { get; set; } = string.Empty;
    
    public Guid CustomerId { get; set; }
}
