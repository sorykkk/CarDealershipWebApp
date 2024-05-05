using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Data.Interfaces;
using CarDealersWebApp.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace CarDealersWebApp.Models.Validation;

public class EmailAttribute : ValidationAttribute
{
    private readonly IUserRepository _userDb = new UserRepository();
    //why is it password??
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value is string email)
        {
            var userTask = _userDb.GetUserByEmail(email);
            userTask.GetAwaiter().GetResult();
            User? user = userTask.Result;

            if (user != null)
                return new ValidationResult("This email is already registered.");
        }

        return ValidationResult.Success;
    }
}
