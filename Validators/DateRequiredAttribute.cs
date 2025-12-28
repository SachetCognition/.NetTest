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
public sealed class DateRequiredAttribute : ValidationAttribute, IClientModelValidator
{
    public DateRequiredAttribute(string errorMessage) : base(errorMessage)
    {
    }

    public DateRequiredAttribute()
    {
    }

    public string? DateTypePropertyName { get; set; }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-daterequired", ErrorMessageString);
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

        var date = value as DateTimeWithFormat;
        
        if (date == null)
        {
            validationResult = new ValidationResult("An error occurred while validating the property. Property is not of type DateTimeWithFormat");
            return validationResult;
        }

        if (validationContext == null)
        {
            throw new ArgumentException("validation context cannot be null");
        }

        if (!string.IsNullOrEmpty(DateTypePropertyName))
        {
            var dateTypePropertyInfo = validationContext.ObjectType.GetProperty(DateTypePropertyName);
            var dateTypeSelected = (string?)dateTypePropertyInfo?.GetValue(validationContext.ObjectInstance, null);
            if (dateTypeSelected != null && !DateComponentHelper.GetDateTypesDouble().Contains(dateTypeSelected))
            {
                return validationResult;
            }
        }

        if (string.IsNullOrEmpty(date.DateText) && string.IsNullOrEmpty(date.HourValue) && string.IsNullOrEmpty(date.MinuteValue))
        {
            validationResult = new ValidationResult(ErrorMessageString);
        }

        return validationResult;
    }
}
