using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators;

/// <summary>
/// Duration data class for validation.
/// </summary>
public class DateDuration
{
    public string? Hour { get; set; }
    public string? Minute { get; set; }
    public string? Second { get; set; }
    public string? MiliSecond { get; set; }
}

/// <summary>
/// The date greater than attribute.
/// This is an example of a custom validator implementation
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class DurationValidatorAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly string _firstPropertyHtmlId;
    private readonly string _firstPropertyName;
    private readonly string _secondPropertyHtmlId;
    private readonly string _secondPropertyName;
    private readonly string _thirdPropertyHtmlId;

    public DurationValidatorAttribute(
        string firstPropertyName, 
        string secondPropertyName, 
        string firstPropertyHtmlId, 
        string secondPropertyHtmlId, 
        string thirdPropertyHtmlId, 
        string errorMessage)
        : base(errorMessage)
    {
        _firstPropertyName = firstPropertyName;
        _secondPropertyName = secondPropertyName;
        _firstPropertyHtmlId = firstPropertyHtmlId;
        _secondPropertyHtmlId = secondPropertyHtmlId;
        _thirdPropertyHtmlId = thirdPropertyHtmlId;
    }

    public DurationValidatorAttribute(
        string firstPropertyName, string secondPropertyName, string firstPropertyHtmlId, string secondPropertyHtmlId, string thirdPropertyHtmlId)
        : this(firstPropertyName, secondPropertyName, firstPropertyHtmlId, secondPropertyHtmlId, thirdPropertyHtmlId, string.Empty)
    {
    }

    public string FirstPropertyHtmlId => _firstPropertyHtmlId;
    public string FirstPropertyName => _firstPropertyName;
    public string SecondPropertyHtmlId => _secondPropertyHtmlId;
    public string SecondPropertyName => _secondPropertyName;
    public string ThirdPropertyHtmlId => _thirdPropertyHtmlId;

    public void AddValidation(ClientModelValidationContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-durationvalidator", ErrorMessageString);
        MergeAttribute(context.Attributes, "data-val-durationvalidator-startdateid", _firstPropertyHtmlId);
        MergeAttribute(context.Attributes, "data-val-durationvalidator-enddateid", _secondPropertyHtmlId);
        MergeAttribute(context.Attributes, "data-val-durationvalidator-datedurationid", _thirdPropertyHtmlId);
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
        if (validationContext != null)
        {
            var inputduration = value as DateDuration;
            var firstPropertyInfo = validationContext.ObjectType.GetProperty(FirstPropertyName);
            var secondPropertyInfo = validationContext.ObjectType.GetProperty(SecondPropertyName);
            var startDate = (DateTimeWithFormat?)firstPropertyInfo?.GetValue(validationContext.ObjectInstance, null);
            var endDate = (DateTimeWithFormat?)secondPropertyInfo?.GetValue(validationContext.ObjectInstance, null);
            
            if (startDate?.Date == null || endDate?.Date == null)
            {
                return validationResult;
            }

            var timediffernce = Convert.ToDateTime(endDate.Date, CultureInfo.InvariantCulture) - Convert.ToDateTime(startDate.Date, CultureInfo.InvariantCulture);
            var datetimeduration = new TimeSpan(
                timediffernce.Days, timediffernce.Hours, timediffernce.Minutes, timediffernce.Seconds, timediffernce.Milliseconds);

            if (inputduration != null)
            {
                var duration = new TimeSpan(
                    0, 
                    Convert.ToInt32(inputduration.Hour, CultureInfo.InvariantCulture), 
                    Convert.ToInt32(inputduration.Minute, CultureInfo.InvariantCulture), 
                    Convert.ToInt32(inputduration.Second, CultureInfo.InvariantCulture), 
                    Convert.ToInt32(inputduration.MiliSecond, CultureInfo.InvariantCulture));

                validationResult = datetimeduration.TotalMilliseconds >= duration.TotalMilliseconds
                                       ? ValidationResult.Success
                                       : new ValidationResult("Invalid Duration");
            }
            else
            {
                validationResult = new ValidationResult("Duration cannot be blank. Please specify the duration");
            }
        }

        return validationResult;
    }
}
