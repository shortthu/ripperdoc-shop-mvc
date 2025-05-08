using System.ComponentModel.DataAnnotations;

namespace RipperdocShop.Web.Models.Auth;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = "";

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; init; } = "";
}
