using System.ComponentModel.DataAnnotations;

namespace Taran.Shared.Dtos.Attributes;

public class RequiredGroupAttribute : RequiredAttribute
{
    private string OtherProperyName { get; set; }

    public RequiredGroupAttribute(string OtherProperyName)
    {
        this.OtherProperyName = OtherProperyName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var currentValidationResult = base.IsValid(value, validationContext);

        validationContext.Items[validationContext.MemberName] = currentValidationResult;

        if (string.IsNullOrWhiteSpace(OtherProperyName) || currentValidationResult == ValidationResult.Success)
            return currentValidationResult;

        ValidationResult? otherPropertyValidationResult;
        if (validationContext.Items.ContainsKey(OtherProperyName))
        {
            otherPropertyValidationResult = (ValidationResult)validationContext.Items[OtherProperyName]!;
        }
        else 
        {
            IsOtherPropertyValid(validationContext, out otherPropertyValidationResult);
            validationContext.Items.Add(OtherProperyName, otherPropertyValidationResult);
        }

        if(otherPropertyValidationResult == ValidationResult.Success)
            return ValidationResult.Success;

        return currentValidationResult;
    }

    private bool IsOtherPropertyValid(ValidationContext validationContext, out ValidationResult? validationResult) 
    {
        var otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperyName)
            ?? throw new Exception($"Other property {OtherProperyName} not found (RequiredOnConditionAttribute)");

        var otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);
        ValidationContext context = new ValidationContext(validationContext.ObjectInstance)
        {
            MemberName = OtherProperyName
        };
        foreach (var item in validationContext.Items)
            context.Items.Add(item.Key, item.Value);
        
        List<ValidationResult> validationResults = new List<ValidationResult>();
        if (Validator.TryValidateProperty(otherPropertyValue, context, validationResults))
        {
            validationResult = ValidationResult.Success;
            return true;
        }

        validationResult = validationResults[0];
        return false;
    }
}
