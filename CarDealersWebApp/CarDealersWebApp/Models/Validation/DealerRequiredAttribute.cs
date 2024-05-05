using CarDealersWebApp.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace CarDealersWebApp.Models.Validation
{
    public class DealerRequiredAttribute : ValidationAttribute
    {
        private readonly string _fieldProperty;
        private readonly string _isDealerProperty;
        public DealerRequiredAttribute(string fieldPropertyName, string isDealerPropertyName) 
        {
            _fieldProperty = fieldPropertyName;
            _isDealerProperty = isDealerPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var fieldProperty = validationContext.ObjectType.GetProperty(_fieldProperty);
            var fieldValue = fieldProperty?.GetValue(validationContext.ObjectInstance, null)?.ToString();

            var isDealerProperty = validationContext.ObjectType.GetProperty(_isDealerProperty);
            var isDealerValue = (bool)(isDealerProperty?.GetValue(validationContext.ObjectInstance) ?? false);

            if (string.IsNullOrWhiteSpace(fieldValue) && isDealerValue)
                return new ValidationResult($"{ _fieldProperty} is required for a dealer.");

            return ValidationResult.Success;
        }

    }
}
