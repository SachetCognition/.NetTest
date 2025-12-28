using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators;

/// <summary>
/// The date greater than attribute.
/// This is an example of a custom validator implementation
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class EndDateGreaterThanAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly string _otherPropertyName;

    public EndDateGreaterThanAttribute(string otherPropertyName, string errorMessage)
         : base(errorMessage)
    {
        _otherPropertyName = otherPropertyName;
    }

    public EndDateGreaterThanAttribute(string otherPropertyName)
        : this(otherPropertyName, string.Empty)
    {
    }

    public string OtherPropertyName => _otherPropertyName;

    public void AddValidation(ClientModelValidationContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-enddategreaterthan", ErrorMessageString);
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

        var endDate = value as DateTimeWithFormat;
        if (endDate == null)
        {
            validationResult = new ValidationResult("An error occurred while validating the property. Property is not of type DateTimeWithFormat");
            return validationResult;
        }

        if (validationContext == null)
        {
            throw new ArgumentException("validation context cannot be null");
        }

        var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);

        if (otherPropertyInfo?.PropertyType != typeof(DateTimeWithFormat))
        {
            validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type DateTimeWithFormat");
            return validationResult;
        }

        var lesserDate = (DateTimeWithFormat?)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

        if (otherPropertyInfo.PropertyType == typeof(DateTimeWithFormat))
        {
            if (lesserDate == null)
            {
                return validationResult;
            }

            if (endDate.Date != null && lesserDate.Date != null)
            {
                if (endDate.Date.Value.CompareTo(lesserDate.Date.Value) < 1)
                {
                    validationResult = new ValidationResult(ErrorMessageString);
                }
            }
        }

        return validationResult;
    }
}
