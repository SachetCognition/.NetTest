using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;
using Equant.SAV2000.ComponentLibrary.MVC.Helpers;

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators;

/// <summary>
/// The date greater than attribute.
/// This is an example of a custom validator implementation
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class EndWeekGreaterThanAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly string _otherPropertyName;
    private readonly string _otherPropertyHtmlId;

    public EndWeekGreaterThanAttribute(string otherPropertyName, string otherPropertyHtmlId, string errorMessage)
        : base(errorMessage)
    {
        _otherPropertyName = otherPropertyName;
        _otherPropertyHtmlId = otherPropertyHtmlId;
    }

    public EndWeekGreaterThanAttribute(string otherPropertyName, string otherPropertyHtmlId)
        : this(otherPropertyName, otherPropertyHtmlId, string.Empty)
    {
    }

    public string OtherPropertyName => _otherPropertyName;
    public string OtherPropertyHtmlId => _otherPropertyHtmlId;

    public void AddValidation(ClientModelValidationContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-endweekgreaterthan", ErrorMessageString);
        MergeAttribute(context.Attributes, "data-val-endweekgreaterthan-lesserweekid", _otherPropertyHtmlId);
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

        var endWeek = value as WeekYearWithFormat;
        if (endWeek == null)
        {
            validationResult = new ValidationResult("An error occurred while validating the property. Property is not of type WeekYearWithFormat");
            return validationResult;
        }

        if (validationContext == null)
        {
            throw new ArgumentException("validation context cannot be null");
        }

        var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);

        if (otherPropertyInfo?.PropertyType != typeof(WeekYearWithFormat))
        {
            validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type WeekYearWithFormat");
            return validationResult;
        }

        var lesserWeek = (WeekYearWithFormat?)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

        if (otherPropertyInfo.PropertyType == typeof(WeekYearWithFormat))
        {
            if (lesserWeek == null)
            {
                return validationResult;
            }

            var endWeekDate = WeekHelper.GetFirstDayOfWeek(endWeek.Week, endWeek.Year);
            var lesserWeekDate = WeekHelper.GetFirstDayOfWeek(lesserWeek.Week, lesserWeek.Year);
            
            if (endWeekDate.CompareTo(lesserWeekDate) < 0)
            {
                validationResult = new ValidationResult(ErrorMessageString);
            }
        }

        return validationResult;
    }
}
