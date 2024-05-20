using CarDealersWebApp.Models.Validation.SignUp;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarDealersWebApp.Models.Auth;

public class RegistrationViewModel
{
    [Required]
    public bool IsDealer { get; set; } = false;

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [DealerRequired(nameof(IsDealer), "Phone")]
    public string? Phone { get; set; }

    [Required(ErrorMessage ="Password is required")]
    //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8}$",ErrorMessage = "Password must meet requirements")]
    [StringLength(20, MinimumLength = 5, ErrorMessage ="Password should be at least 5 characters.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage ="Password confirmation is required")]
    [ConfirmPassword(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage ="Email is required")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [DealerRequired(nameof(IsDealer), "Address")]
    public string? Address { get; set; }

    [DealerRequired(nameof(IsDealer), "Country")]
    public string? Country { get; set; }

    [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree before submitting.")]
    public bool Agreement { get; set; }
}
