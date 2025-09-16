using System.ComponentModel.DataAnnotations;

namespace Taran.Shared.Dtos.Attributes;

public class MobileAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if(value is null)
            return ValidationResult.Success;

        if (string.IsNullOrWhiteSpace(value + ""))
            return ValidationResult.Success;

        if (!value.ToString().StartsWith("09") || value.ToString().Length != 11)
            return new ValidationResult(ErrorMessage, new List<string> { validationContext.MemberName });

        return ValidationResult.Success;
    }
}
