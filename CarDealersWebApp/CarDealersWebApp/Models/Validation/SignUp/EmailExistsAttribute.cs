using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Data.Interfaces;
using CarDealersWebApp.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace CarDealersWebApp.Models.Validation.SignUp;

public class EmailExistsAttribute : ValidationAttribute
{
    private readonly IUserRepository _userDb;
    public EmailExistsAttribute()
    {
        _userDb = new UserRepository();
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string email)
        {
            var userTask = _userDb.GetUserByEmail(email);
            userTask.GetAwaiter().GetResult();
            User? user = userTask.Result;

            if (user != null)
                return new ValidationResult("This email already exists.");
        }

        return ValidationResult.Success;
    }
}
