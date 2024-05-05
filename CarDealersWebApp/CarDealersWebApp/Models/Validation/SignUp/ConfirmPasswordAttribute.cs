using System.ComponentModel.DataAnnotations;

namespace CarDealersWebApp.Models.Validation.SignUp
{
    public class ConfirmPasswordAttribute : ValidationAttribute
    {
        private readonly string _passwordProperty;
        public ConfirmPasswordAttribute(string passwordPropertyName = "Password")
        {
            _passwordProperty = passwordPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var passwordProperty = validationContext.ObjectType.GetProperty(_passwordProperty);
            var passValue = passwordProperty?.GetValue(validationContext.ObjectInstance, null)?.ToString();

            if (string.IsNullOrWhiteSpace(passValue))
            {
                return new ValidationResult("No password was specified.");
            }

            var confirmPassValue = value?.ToString();
            if (string.IsNullOrWhiteSpace(confirmPassValue))
            {
                return new ValidationResult("No confirmation password was specified.");
            }

            if (!passValue.Equals(confirmPassValue))
            {
                return new ValidationResult("Confirm password does not match.");
            }

            return ValidationResult.Success;
        }
    }
}
