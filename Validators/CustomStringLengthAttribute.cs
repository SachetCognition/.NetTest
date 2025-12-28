using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators;

/// <summary>
/// Specifies the minimum and maximum length of characters that are allowed in a data field
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class CustomStringLengthAttribute : StringLengthAttribute, IClientModelValidator
{
    public CustomStringLengthAttribute(int maximumLength)
        : base(maximumLength)
    {
    }
  
    public override bool IsValid(object? value)
    {
        return base.IsValid(Convert.ToString(value, CultureInfo.CurrentCulture));
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-length", ErrorMessageString);
        MergeAttribute(context.Attributes, "data-val-length-max", MaximumLength.ToString(CultureInfo.InvariantCulture));
        if (MinimumLength > 0)
        {
            MergeAttribute(context.Attributes, "data-val-length-min", MinimumLength.ToString(CultureInfo.InvariantCulture));
        }
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
}
