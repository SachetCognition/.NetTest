// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeWithFormatBinder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 12/05/2014
//   Author:  Sharma Siddharth (54626)
//   Description: This class defines custom model binder to handle date with format.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

    /// <summary>
    /// The custom model binder for the date time with format class.
    /// </summary>
    public class DateTimeWithFormatBinder : DefaultModelBinder
    {
        /// <summary>
        /// This method binds the model.
        /// </summary>
        /// <param name="controllerContext">
        /// The controller context.
        /// </param>
        /// <param name="bindingContext">
        /// The binding context.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext");
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

            //if any of the date or format values could not be retrieved then there is serious error with the form data
            if (dateProviderResult == null || formatProviderResult == null || typeProviderResult == null || timeOffsetProviderResult == null
                || utcProviderResult == null)
            {
                var exceptionMessage =
                    "There is serious error with the submitted data. ".AppendWithBuilder(
                        "Either or all of the date, format, type, timeOffset and utc values are missing for the model name ",
                        modelName,
                        ". It can occur if the form data has been maliciously modified.");
                throw new DateTimeWithFormatException(exceptionMessage);
            }

            var dateValue = dateProviderResult.AttemptedValue;
            var formatValue = formatProviderResult.AttemptedValue;
            var typeValue = typeProviderResult.AttemptedValue;
            var timeOffsetValue = timeOffsetProviderResult.AttemptedValue;
            var utcValue = utcProviderResult.AttemptedValue;

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

            var hourValue = hourProviderResult != null ? hourProviderResult.AttemptedValue : string.Empty;
            var minuteValue = minuteProviderResult != null ? minuteProviderResult.AttemptedValue : string.Empty;

            DateTimeWithFormat dateTimeValue;

            if (string.IsNullOrEmpty(dateValue))
            {
                //if date is empty but time is present, then it is incomplete date and is a model state error
                if (!string.IsNullOrEmpty(hourValue) || !string.IsNullOrEmpty(minuteValue))
                {
                    bindingContext.ModelState.AddModelError(modelName, ApplicationStrings.ERR_DATE_INVALIDE);
                }

                dateTimeValue = new DateTimeWithFormat(formatValue, isModel) { DateText = string.Empty, TimeOffset = timeOffset, IsUtcMode = isUtcMode };
                PutDateInModelState(bindingContext, modelName, dateTimeValue);
                return dateTimeValue;
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
                // Important: if the date is not a valid date, then hour and minute must be empty. This is to replicate the behavior from client side.
                // If this is not done, then it will create dissimilarity in behavior on server side from the client side.
                dateTimeValue = new DateTimeWithFormat(formatValue, isModel) { DateText = dateValue, TimeOffset = timeOffset, IsUtcMode = isUtcMode };
                PutDateInModelState(bindingContext, modelName, dateTimeValue);
                return dateTimeValue;
            }

            //if date is not empty and exactly one of the time is not submit, then it is incomplete time and is a model state error
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
                PutDateInModelState(bindingContext, modelName, dateTimeValue);
                return dateTimeValue;
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
                    PutDateInModelState(bindingContext, modelName, dateTimeValue);
                    return dateTimeValue;
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
                PutDateInModelState(bindingContext, modelName, dateTimeValue);
                return dateTimeValue;
            }

            dateTimeValue = new DateTimeWithFormat(formatValue, isModel)
                       {
                           DateText = dateValue,
                           HourValue = hourValue,
                           MinuteValue = minuteValue,
                           TimeOffset = timeOffset,
                           IsUtcMode = isUtcMode
                       };
            PutDateInModelState(bindingContext, modelName, dateTimeValue);
            return dateTimeValue;
        }

        /// <summary>
        /// This puts the parsed date time with format value into the model state.
        /// </summary>
        /// <param name="bindingContext">
        /// The binding context.
        /// </param>
        /// <param name="modelName">
        /// The model name.
        /// </param>
        /// <param name="dateTimeValue">
        /// The date time value.
        /// </param>
        private static void PutDateInModelState(ModelBindingContext bindingContext, string modelName, DateTimeWithFormat dateTimeValue)
        {
            bindingContext.ModelState.Remove(modelName);
            bindingContext.ModelState.Add(modelName, new ModelState());
            bindingContext.ModelState.SetModelValue(modelName, new ValueProviderResult(dateTimeValue, dateTimeValue.ToString(), null));
        }

        /// <summary>
        /// The check for valid UTC.
        /// </summary>
        /// <param name="utcValue">
        /// The UTC value.
        /// </param>
        /// <param name="modelName">
        /// The model name.
        /// </param>
        /// <exception cref="DateTimeWithFormatException">
        /// </exception>
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

        /// <summary>
        /// The get time offset.
        /// </summary>
        /// <param name="timeOffsetValue">
        /// The time offset value.
        /// </param>
        /// <param name="modelName">
        /// The model name.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        /// <exception cref="DateTimeWithFormatException">
        /// </exception>
        private static double GetTimeOffset(string timeOffsetValue, string modelName)
        {
            Double timeOffset;
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

        /// <summary>
        /// The check for valid type.
        /// </summary>
        /// <param name="typeValue">
        /// The type value.
        /// </param>
        /// <param name="modelName">
        /// The model name.
        /// </param>
        /// <exception cref="DateTimeWithFormatException">
        /// </exception>
        private static void CheckForValidType(string typeValue, string modelName)
        {
            //if model field is missing then date with format field cannot be parsed
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

        /// <summary>
        /// The check for valid format.
        /// </summary>
        /// <param name="formatValue">
        /// The format value.
        /// </param>
        /// <param name="modelName">
        /// The model name.
        /// </param>
        /// <exception cref="DateTimeWithFormatException">
        /// </exception>
        private static void CheckForValidFormat(string formatValue, string modelName)
        {
            //if format is missing then date with format field cannot be parsed
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
}
