using System.ComponentModel.DataAnnotations;

namespace Taran.Shared.Dtos.Attributes;

public class TimeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(value + ""))
            return ValidationResult.Success;

        TimeOnly time = TimeOnly.MinValue;
        if (!TimeOnly.TryParse(value.ToString(), out time))
            return new ValidationResult(ErrorMessage, new List<string> { validationContext.MemberName });

        return ValidationResult.Success;
    }
}