using System.ComponentModel.DataAnnotations;
using RipperdocShop.Models.Identities;

namespace RipperdocShop.Models.Entities;

public class ProductRating
{
    public Guid Id { get; private set; }
    
    [Required]
    [Range(1, 5)]
    public int Score { get; private set; }
    public string? Comment { get; private set; } = string.Empty;
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = null!;
    
    public Guid UserId { get; private set; }
    public AppUser User { get; private set; } = null!;
    
    public ProductRating() { }

    public ProductRating(int score, string? comment, Product product, AppUser user)
    {
        Id = Guid.NewGuid();
        Score = score;
        Comment = comment;
        ProductId = product.Id;
        Product = product;
        UserId = user.Id;
        User = user;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}