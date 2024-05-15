using System.ComponentModel.DataAnnotations;

namespace CarDealersWebApp.Models.Auth;

public class LoginViewModel
{
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;

    //aici va trebui ceva cu claim
    public bool IsLogged { get; set; } = false;
}
