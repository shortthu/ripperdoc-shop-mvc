using RipperdocShop.Models.Identity;

namespace RipperdocShop.Models.Entities;

public enum OrderStatus
{
    Pending,
    Shipping,
    Completed,
    Cancelled,
}

public class Order
{
    public Guid Id { get; private set; }
    public decimal TotalPrice { get; private set; }
    public OrderStatus Status { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public Guid UserId { get; private set; }
    public AppUser User { get; private set; } = null!;
    
    public Order() { }
 }