using System.ComponentModel.DataAnnotations;

namespace RipperdocShop.Web.Models.Auth;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = "";

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; } = "";

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; init; } = "";
}
