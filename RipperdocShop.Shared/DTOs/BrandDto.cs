using System.ComponentModel.DataAnnotations;

namespace RipperdocShop.Shared.DTOs;

public class BrandDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
}
