namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

    public class DateTimeWithFormatBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;
            var datePropertyName = modelName.AppendWithBuilder(".", "Date");
            var formatPropertyName = modelName.AppendWithBuilder(".", "Format");
            var typePropertyName = modelName.AppendWithBuilder(".", "Type");
            var timeOffsetPropertyName = modelName.AppendWithBuilder(".", "TimeOffset");
            var utcPropertyName = modelName.AppendWithBuilder(".", "Utc");

            var dateProviderResult = bindingContext.ValueProvider.GetValue(datePropertyName);
            var formatProviderResult = bindingContext.ValueProvider.GetValue(formatPropertyName);
            var typeProviderResult = bindingContext.ValueProvider.GetValue(typePropertyName);
            var timeOffsetProviderResult = bindingContext.ValueProvider.GetValue(timeOffsetPropertyName);
            var utcProviderResult = bindingContext.ValueProvider.GetValue(utcPropertyName);

            if (dateProviderResult == ValueProviderResult.None || formatProviderResult == ValueProviderResult.None
                || typeProviderResult == ValueProviderResult.None || timeOffsetProviderResult == ValueProviderResult.None
                || utcProviderResult == ValueProviderResult.None)
            {
                throw new DateTimeWithFormatException(
                    "There is serious error with the submitted data. " +
                    "Either or all of the date, format, type, timeOffset and utc values are missing for the model name " +
                    modelName);
            }

            var dateValue = dateProviderResult.FirstValue;
            var formatValue = formatProviderResult.FirstValue;
            var typeValue = typeProviderResult.FirstValue;
            var timeOffsetValue = timeOffsetProviderResult.FirstValue;
            var utcValue = utcProviderResult.FirstValue;

            CheckForValidFormat(formatValue, modelName);
            CheckForValidType(typeValue, modelName);
            var timeOffset = GetTimeOffset(timeOffsetValue, modelName);
            CheckForValidUtc(utcValue, modelName);

            var isUtcMode = utcValue == DateTimeConstants.IsUtc;
            var isModel = (typeValue == DateTimeConstants.ModelFormat);

            var hourPropertyName = modelName.AppendWithBuilder(".", "DropDownHours");
            var minutePropertyName = modelName.AppendWithBuilder(".", "DropDownMins");
            var hourProviderResult = bindingContext.ValueProvider.GetValue(hourPropertyName);
            var minuteProviderResult = bindingContext.ValueProvider.GetValue(minutePropertyName);
            var hourValue = hourProviderResult != ValueProviderResult.None ? hourProviderResult.FirstValue : string.Empty;
            var minuteValue = minuteProviderResult != ValueProviderResult.None ? minuteProviderResult.FirstValue : string.Empty;

            DateTimeWithFormat dateTimeValue;

            if (string.IsNullOrEmpty(dateValue))
            {
                if (!string.IsNullOrEmpty(hourValue) || !string.IsNullOrEmpty(minuteValue))
                {
                    bindingContext.ModelState.AddModelError(modelName, ApplicationStrings.ERR_DATE_INVALIDE);
                }
                dateTimeValue = new DateTimeWithFormat(formatValue, isModel) { DateText = string.Empty, TimeOffset = timeOffset, IsUtcMode = isUtcMode };
                bindingContext.Result = ModelBindingResult.Success(dateTimeValue);
                return Task.CompletedTask;
            }

            DateTime parsedDate;
            bool isDate;
            if (typeValue == DateTimeConstants.StandardFormat)
            {
                isDate = DateTime.TryParseExact(dateValue, formatValue, null, DateTimeStyles.None, out parsedDate);
            }
            else
            {
                parsedDate = DateComponentHelper.ParseModelDate(dateValue, formatValue, timeOffset, isUtcMode);
                isDate = (parsedDate != DateTime.MinValue);
            }

            if (!isDate)
            {
                bindingContext.ModelState.AddModelError(modelName, ApplicationStrings.ERR_DATE_INVALIDE);
                dateTimeValue = new DateTimeWithFormat(formatValue, isModel) { DateText = dateValue, TimeOffset = timeOffset, IsUtcMode = isUtcMode };
                bindingContext.Result = ModelBindingResult.Success(dateTimeValue);
                return Task.CompletedTask;
            }

            if (string.IsNullOrEmpty(hourValue) ^ string.IsNullOrEmpty(minuteValue))
            {
                bindingContext.ModelState.AddModelError(modelName, ApplicationStrings.TimeMandatory);
                dateTimeValue = new DateTimeWithFormat(formatValue, isModel)
                {
                    DateText = dateValue,
                    HourValue = hourValue,
                    MinuteValue = minuteValue,
                    TimeOffset = timeOffset,
                    IsUtcMode = isUtcMode
                };
                bindingContext.Result = ModelBindingResult.Success(dateTimeValue);
                return Task.CompletedTask;
            }

            if (!string.IsNullOrEmpty(hourValue) && !string.IsNullOrEmpty(minuteValue))
            {
                if (!int.TryParse(hourValue, out int hourParsed) || hourParsed < 0 || hourParsed > 23 ||
                    !int.TryParse(minuteValue, out int minuteParsed) || minuteParsed < 0 || minuteParsed > 59)
                {
                    bindingContext.ModelState.AddModelError(modelName, ApplicationStrings.TimeMandatory);
                    dateTimeValue = new DateTimeWithFormat(formatValue, isModel)
                    {
                        DateText = dateValue,
                        HourValue = hourValue,
                        MinuteValue = minuteValue,
                        TimeOffset = timeOffset,
                        IsUtcMode = isUtcMode
                    };
                    bindingContext.Result = ModelBindingResult.Success(dateTimeValue);
                    return Task.CompletedTask;
                }
            }

            dateTimeValue = new DateTimeWithFormat(formatValue, isModel)
            {
                DateText = dateValue,
                HourValue = hourValue,
                MinuteValue = minuteValue,
                TimeOffset = timeOffset,
                IsUtcMode = isUtcMode
            };
            bindingContext.Result = ModelBindingResult.Success(dateTimeValue);
            return Task.CompletedTask;
        }

        private static void CheckForValidUtc(string utcValue, string modelName)
        {
            if (utcValue != DateTimeConstants.IsUtc && utcValue != DateTimeConstants.IsNonUtc)
                throw new DateTimeWithFormatException("Invalid UTC value for model " + modelName);
        }

        private static double GetTimeOffset(string timeOffsetValue, string modelName)
        {
            if (!double.TryParse(timeOffsetValue, out double timeOffset))
                throw new DateTimeWithFormatException("Invalid timeOffset value for model " + modelName);
            return timeOffset;
        }

        private static void CheckForValidType(string typeValue, string modelName)
        {
            if (string.IsNullOrEmpty(typeValue) || (typeValue != DateTimeConstants.StandardFormat && typeValue != DateTimeConstants.ModelFormat))
                throw new DateTimeWithFormatException("Invalid type value for model " + modelName);
        }

        private static void CheckForValidFormat(string formatValue, string modelName)
        {
            if (string.IsNullOrEmpty(formatValue) || (formatValue != DateTimeConstants.EnglishFormat && formatValue != DateTimeConstants.FrenchFormat))
                throw new DateTimeWithFormatException("Invalid format value for model " + modelName);
        }
    }
}
