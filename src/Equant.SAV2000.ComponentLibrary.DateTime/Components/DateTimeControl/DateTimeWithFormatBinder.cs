namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// ASP.NET Core model binder for DateTimeWithFormat.
    /// Replaces the legacy DefaultModelBinder-based binder from System.Web.Mvc.
    /// </summary>
    public class DateTimeWithFormatBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            var datePropertyName = string.IsNullOrEmpty(modelName) ? "Date" : modelName + ".Date";
            var formatPropertyName = string.IsNullOrEmpty(modelName) ? "Format" : modelName + ".Format";
            var typePropertyName = string.IsNullOrEmpty(modelName) ? "Type" : modelName + ".Type";
            var timeOffsetPropertyName = string.IsNullOrEmpty(modelName) ? "TimeOffset" : modelName + ".TimeOffset";
            var utcPropertyName = string.IsNullOrEmpty(modelName) ? "Utc" : modelName + ".Utc";
            var hourPropertyName = string.IsNullOrEmpty(modelName) ? "Hour" : modelName + ".Hour";
            var minutePropertyName = string.IsNullOrEmpty(modelName) ? "Minute" : modelName + ".Minute";

            var dateValue = bindingContext.ValueProvider.GetValue(datePropertyName);
            var formatValue = bindingContext.ValueProvider.GetValue(formatPropertyName);
            var typeValue = bindingContext.ValueProvider.GetValue(typePropertyName);
            var timeOffsetValue = bindingContext.ValueProvider.GetValue(timeOffsetPropertyName);
            var utcValue = bindingContext.ValueProvider.GetValue(utcPropertyName);
            var hourValue = bindingContext.ValueProvider.GetValue(hourPropertyName);
            var minuteValue = bindingContext.ValueProvider.GetValue(minutePropertyName);

            // Parse format
            var format = formatValue.FirstValue;
            if (string.IsNullOrEmpty(format))
            {
                format = DateTimeConstants.EnglishFormat;
            }

            // Parse type
            var type = typeValue.FirstValue;
            bool isModel = string.Equals(type, DateTimeConstants.ModelFormat, StringComparison.OrdinalIgnoreCase);

            var result = new DateTimeWithFormat(format, isModel);

            // Set date text
            if (dateValue != ValueProviderResult.None && !string.IsNullOrEmpty(dateValue.FirstValue))
            {
                result.DateText = dateValue.FirstValue;
            }

            // Set hour
            if (hourValue != ValueProviderResult.None && !string.IsNullOrEmpty(hourValue.FirstValue))
            {
                result.HourValue = hourValue.FirstValue;
            }

            // Set minute
            if (minuteValue != ValueProviderResult.None && !string.IsNullOrEmpty(minuteValue.FirstValue))
            {
                result.MinuteValue = minuteValue.FirstValue;
            }

            // Set UTC mode
            if (utcValue != ValueProviderResult.None && !string.IsNullOrEmpty(utcValue.FirstValue))
            {
                result.IsUtcMode = string.Equals(utcValue.FirstValue, DateTimeConstants.IsUtc, StringComparison.OrdinalIgnoreCase);
            }

            // Set time offset
            if (timeOffsetValue != ValueProviderResult.None && !string.IsNullOrEmpty(timeOffsetValue.FirstValue))
            {
                if (double.TryParse(timeOffsetValue.FirstValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double offset))
                {
                    result.TimeOffset = offset;
                }
            }

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
