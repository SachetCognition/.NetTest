using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.Common.Resources;
using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
using Equant.SAV2000.ComponentLibrary.MVC.Helpers;
using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;

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

        if (dateProviderResult == ValueProviderResult.None || 
            formatProviderResult == ValueProviderResult.None || 
            typeProviderResult == ValueProviderResult.None || 
            timeOffsetProviderResult == ValueProviderResult.None ||
            utcProviderResult == ValueProviderResult.None)
        {
            var exceptionMessage =
                "There is serious error with the submitted data. ".AppendWithBuilder(
                    "Either or all of the date, format, type, timeOffset and utc values are missing for the model name ",
                    modelName,
                    ". It can occur if the form data has been maliciously modified.");
            throw new DateTimeWithFormatException(exceptionMessage);
        }

        var dateValue = dateProviderResult.FirstValue ?? string.Empty;
        var formatValue = formatProviderResult.FirstValue ?? string.Empty;
        var typeValue = typeProviderResult.FirstValue ?? string.Empty;
        var timeOffsetValue = timeOffsetProviderResult.FirstValue ?? string.Empty;
        var utcValue = utcProviderResult.FirstValue ?? string.Empty;

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

        var hourValue = hourProviderResult != ValueProviderResult.None ? hourProviderResult.FirstValue ?? string.Empty : string.Empty;
        var minuteValue = minuteProviderResult != ValueProviderResult.None ? minuteProviderResult.FirstValue ?? string.Empty : string.Empty;

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

        int hourParsed;
        var isHourParsed = int.TryParse(hourValue, out hourParsed);
        if (isHourParsed && (hourParsed <= 23 && hourParsed >= 0))
        {
            int minuteParsed;
            var isMinuteParsed = int.TryParse(minuteValue, out minuteParsed);
            if (!isMinuteParsed || !(minuteParsed <= 59 && minuteParsed >= 0))
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
        else
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
        {
            var exceptionMessage =
                "There is serious error with the submitted data. ".AppendWithBuilder(
                    "The value for the UTC input is not a permitted value for the model name ",
                    modelName,
                    ". It can occur if the form data has been maliciously modified.");
            throw new DateTimeWithFormatException(exceptionMessage);
        }
    }

    private static double GetTimeOffset(string timeOffsetValue, string modelName)
    {
        double timeOffset;
        var isTimeOffset = double.TryParse(timeOffsetValue, out timeOffset);

        if (!isTimeOffset)
        {
            var exceptionMessage =
                "There is serious error with the submitted data. ".AppendWithBuilder(
                    "The value for the timeOffset input is not a permitted value for the model name ",
                    modelName,
                    ". It can occur if the form data has been maliciously modified.");
            throw new DateTimeWithFormatException(exceptionMessage);
        }

        return timeOffset;
    }

    private static void CheckForValidType(string typeValue, string modelName)
    {
        if (string.IsNullOrEmpty(typeValue) || (typeValue != DateTimeConstants.StandardFormat && typeValue != DateTimeConstants.ModelFormat))
        {
            var exceptionMessage =
                "There is serious error with the submitted data. ".AppendWithBuilder(
                    "The value for the type input is not a permitted value for the model name ",
                    modelName,
                    ". It can occur if the form data has been maliciously modified.");
            throw new DateTimeWithFormatException(exceptionMessage);
        }
    }

    private static void CheckForValidFormat(string formatValue, string modelName)
    {
        if (string.IsNullOrEmpty(formatValue) || (formatValue != DateTimeConstants.EnglishFormat && formatValue != DateTimeConstants.FrenchFormat))
        {
            var exceptionMessage =
                "There is serious error with the submitted data. ".AppendWithBuilder(
                    "The value for the format input is not a permitted value for the model name ",
                    modelName,
                    ". It can occur if the form data has been maliciously modified.");
            throw new DateTimeWithFormatException(exceptionMessage);
        }
    }
}
