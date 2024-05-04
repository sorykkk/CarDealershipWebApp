using CarDealersWebApp.Models.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarDealersWebApp.Models.Auth
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Custom val msg")]
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }

        [Required]
        [MinLength(10)]
        public string Password { get; set; } = string.Empty;

        [ConfirmPassword(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
