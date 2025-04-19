using System.ComponentModel.DataAnnotations;

namespace RipperdocShop.Models.ViewModels;

public class CategoryViewModel
{
    public Guid? Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
}