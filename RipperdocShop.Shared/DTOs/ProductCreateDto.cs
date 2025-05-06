using System.ComponentModel.DataAnnotations;

namespace RipperdocShop.Shared.DTOs;

public class ProductCreateDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [Url]
    public string ImageUrl { get; set; } = string.Empty;
    
    [Range(0.0, double.MaxValue)]
    public decimal Price { get; set; }

    public bool IsFeatured { get; set; } = false;

    [Required]
    public Guid CategoryId { get; set; }
    
    public Guid? BrandId { get; set; }
}
