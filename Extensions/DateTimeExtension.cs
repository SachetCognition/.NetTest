// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtension.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 04/07/2014
//   Author:  Sharma Siddharth (54626)
//   Description: This defines extensions for DateTime.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Extensions
{
    using System;
    using System.Globalization;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

    /// <summary>
    /// The date time extension.
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// The get date time with format.
        /// </summary>
        /// <param name="dateTime">
        /// The date time.
        /// </param>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeWithFormat"/>.
        /// </returns>
        public static DateTimeWithFormat GetDateTimeWithFormat(this DateTime? dateTime, string format)
        {
            if (format != DateTimeConstants.FrenchFormat && format != DateTimeConstants.EnglishFormat)
            {
                return null;
            }

            var dateText = string.Empty;
            var hourValue = string.Empty;
            var minuteValue = string.Empty;

            if (dateTime == null)
            {
                return new DateTimeWithFormat(format, false) { DateText = dateText, HourValue = hourValue, MinuteValue = minuteValue };
            }

            string textFormat = format == DateTimeConstants.EnglishFormat ? "MM/dd/yyyy" : "dd/MM/yyyy";

            dateText = dateTime.Value.ToString(textFormat, CultureInfo.InvariantCulture);
            hourValue = dateTime.Value.Hour.ToString(CultureInfo.InvariantCulture);
            minuteValue = dateTime.Value.Minute.ToString(CultureInfo.InvariantCulture);

            return new DateTimeWithFormat(format, false) { DateText = dateText, HourValue = hourValue, MinuteValue = minuteValue };
        }
    }
}
