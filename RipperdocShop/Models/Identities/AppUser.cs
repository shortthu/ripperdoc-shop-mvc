using Microsoft.AspNetCore.Identity;
using RipperdocShop.Models.Entities;

namespace RipperdocShop.Models.Identities;

public class AppUser : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public bool IsDisabled { get; private set; }
    public List<ProductRating> ProductRatings { get; private set; } = new();
    
    public AppUser() { }
    
    public void SoftDelete()
    {if (DeletedAt != null)
            throw new InvalidOperationException("Already flatlined, choom. (User is already deleted)");
        
        DeletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Restore()
    {
        if (DeletedAt == null)
            throw new InvalidOperationException("They're still alive, y'know. (User is not deleted)");
        
        DeletedAt = null;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Disable()
    {
        if (IsDisabled)
            throw new InvalidOperationException("Nah, already banned. (User is already disabled.)");
        
        IsDisabled = true;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Enable()
    {
        if (!IsDisabled)
            throw new InvalidOperationException("They're still active. (User is not disabled.)");
        
        IsDisabled = false;
        UpdatedAt = DateTime.UtcNow;
    }
}