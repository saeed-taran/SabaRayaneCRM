using Taran.Shared.Dtos.Services.Calendar;
using System.ComponentModel.DataAnnotations;

namespace Taran.Shared.Dtos.Attributes;

public class DateAttribute : ValidationAttribute
{
    // Blazor's dependency injection is not working for attributes, this is a temporary solution for injecting icalendar into DateAttribute
    public static ICalendar Calendar;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(value + ""))
            return ValidationResult.Success;

        DateTime date = DateTime.MinValue;
        if (!Calendar.TryParse(value.ToString(), out date))
            return new ValidationResult(ErrorMessage, new List<string> { validationContext.MemberName });

        return ValidationResult.Success;
    }
}