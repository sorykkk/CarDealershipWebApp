using CarDealersWebApp.Data.Entities;
using CarDealersWebApp.Data.Interfaces;
using CarDealersWebApp.Data.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace CarDealersWebApp.Models.Validation.LogIn;

public class IncorrectCredentialsAttribute : ValidationAttribute
{
    private readonly IUserRepository _userDb;
    private readonly string _emailProperty;
    public IncorrectCredentialsAttribute(string emailPropertyName)
    {
        _userDb = new UserRepository();
        _emailProperty = emailPropertyName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var emailProperty = validationContext.ObjectType.GetProperty(_emailProperty);
        var emailValue = emailProperty?.GetValue(validationContext.ObjectInstance, null)?.ToString();

        var userTask = _userDb.GetUserByEmail(emailValue);
        userTask.GetAwaiter().GetResult();
        User? user = userTask.Result;

        if(user == null)
        {
            return new ValidationResult("Incorrect email or password.");
        }
        else
        {
            var passValue = value?.ToString();
            if (string.IsNullOrWhiteSpace(passValue))
            {
                return new ValidationResult("No password was specified.");
            }
            string hashedPassValue;
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passValue));
            hashedPassValue = BitConverter.ToString(bytes).Replace("-", "").ToLower();

            if (!hashedPassValue.Equals(user.Password))
            {
                return new ValidationResult("Incorrect email or password.");
            }
        }

        return ValidationResult.Success;
    }
}
