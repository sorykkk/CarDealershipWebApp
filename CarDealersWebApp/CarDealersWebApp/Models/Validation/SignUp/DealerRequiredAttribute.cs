using CarDealersWebApp.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace CarDealersWebApp.Models.Validation.SignUp;

public class DealerRequiredAttribute : ValidationAttribute
{
    private string _filedName;
    private readonly string _isDealerPropertyName;

    public DealerRequiredAttribute(string isDealerPropertyName, string fieldName)
    {
        _filedName = fieldName;
        _isDealerPropertyName = isDealerPropertyName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var isDealerProperty = validationContext.ObjectType.GetProperty(_isDealerPropertyName);
        var isDealerValue = (bool)(isDealerProperty?.GetValue(validationContext.ObjectInstance) ?? false);

        if (isDealerValue)
        {
            var fieldNameValue = value?.ToString();
            if(string.IsNullOrWhiteSpace(fieldNameValue))
            {
                return new ValidationResult($"{_filedName} is required for a dealer.");
            }
        }

        return ValidationResult.Success;
    }
}
