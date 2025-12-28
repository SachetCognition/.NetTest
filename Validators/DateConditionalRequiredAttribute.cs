using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
using Equant.SAV2000.ComponentLibrary.MVC.Helpers;

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators;

/// <summary>
/// The date greater than attribute.
/// This is an example of a custom validator implementation
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class DateConditionalRequiredAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly string _otherPropertyName;

    public DateConditionalRequiredAttribute(string otherPropertyName, string errorMessage)
        : base(errorMessage)
    {
        _otherPropertyName = otherPropertyName;
    }

    public DateConditionalRequiredAttribute(string otherPropertyName)
        : this(otherPropertyName, string.Empty)
    {
    }

    public string OtherPropertyName => _otherPropertyName;

    public string? DateTypePropertyName { get; set; }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-dateconditionalrequired", ErrorMessageString);
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

        var thisDate = value as DateTimeWithFormat;
        if (thisDate == null)
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

        var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);

        if (otherPropertyInfo?.PropertyType != typeof(DateTimeWithFormat))
        {
            validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type DateTimeWithFormat");
            return validationResult;
        }

        var otherDate = (DateTimeWithFormat?)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

        if (otherPropertyInfo.PropertyType == typeof(DateTimeWithFormat))
        {
            if (otherDate == null)
            {
                return validationResult;
            }

            if (otherDate.Date != null && string.IsNullOrEmpty(thisDate.DateText) && string.IsNullOrEmpty(thisDate.HourValue) && 
                string.IsNullOrEmpty(thisDate.MinuteValue))
            {
                validationResult = new ValidationResult(ErrorMessageString);
            }
        }

        return validationResult;
    }
}
