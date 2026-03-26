namespace Equant.SAV2000.ComponentLibrary.MVC.Extensions
{
    using System;
    using System.Globalization;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

    public static class DateTimeExtension
    {
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
