using Microsoft.AspNetCore.Identity;

namespace RipperdocShop.Models.Identity;

public class AppUser : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public bool IsDisabled { get; private set; }
    
    public AppUser() { }
}