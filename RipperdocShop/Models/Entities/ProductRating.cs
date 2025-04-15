using RipperdocShop.Models.Identity;

namespace RipperdocShop.Models.Entities;

public class ProductRating
{
    public Guid Id { get; private set; }
    public int Score { get; private set; }
    public string Comment { get; private set; } = string.Empty;
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = null!;
    
    public Guid UserId { get; private set; }
    public AppUser User { get; private set; } = null!;
    
    public ProductRating() { }
}