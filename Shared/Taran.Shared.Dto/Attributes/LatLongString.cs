using System.ComponentModel.DataAnnotations;

namespace Taran.Shared.Dtos.Attributes;

public class LatLongString : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if(value is null)
            return ValidationResult.Success;

        if (string.IsNullOrWhiteSpace(value + ""))
            return ValidationResult.Success;

        if (!value.ToString().Contains(','))
            return new ValidationResult(ErrorMessage, new List<string> { validationContext.MemberName });

        var splitedValues = value.ToString().Split(',');

        decimal temp;
        if (!decimal.TryParse(splitedValues[0], out temp))
            return new ValidationResult(ErrorMessage, new List<string> { validationContext.MemberName });

        if (!decimal.TryParse(splitedValues[1], out temp))
            return new ValidationResult(ErrorMessage, new List<string> { validationContext.MemberName });

        return ValidationResult.Success;
    }
}
