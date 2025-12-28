using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
using Equant.SAV2000.ComponentLibrary.MVC.Helpers;

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators;

/// <summary>
/// The date required attribute.
/// This is date required attribute class
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class LessThanCurrentDateAttribute : ValidationAttribute, IClientModelValidator
{
    public LessThanCurrentDateAttribute(string errorMessage) : base(errorMessage)
    {
    }

    public LessThanCurrentDateAttribute()
    {
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-lessthancurrentdate", ErrorMessageString);
    }

    private static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
    {
        if (attributes.ContainsKey(key))
        {
            return false;
        }
        attributes.Add(key, value);
        return true;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var validationResult = ValidationResult.Success;
        if (value == null)
        {
            return validationResult;
        }

        var dateTimeValue = value as DateTimeWithFormat;
        
        if (dateTimeValue == null)
        {
            validationResult = new ValidationResult("An error occurred while validating the property. Property is not of type DateTimeWithFormat");
            return validationResult;
        }

        if (dateTimeValue.Date != null)
        {
            var currentDate = DateComponentHelper.GetCurrentDate(dateTimeValue);
            if (dateTimeValue.Date.Value < currentDate)
            {
                validationResult = new ValidationResult(ErrorMessageString);
            }
        }

        return validationResult;
    }
}
