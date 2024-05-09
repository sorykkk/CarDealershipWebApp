using System.ComponentModel.DataAnnotations;
using CarDealersWebApp.Models.Validation.LogIn;

namespace CarDealersWebApp.Models.Auth;

public class LoginViewModel
{
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;

    //[IncorrectCredentials(nameof(Email))]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;

    public bool IsLogged { get; set; } = false;
}
