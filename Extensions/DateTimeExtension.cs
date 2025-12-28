using System.Globalization;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

namespace Equant.SAV2000.ComponentLibrary.MVC.Extensions;

/// <summary>
/// Extension methods for DateTime operations.
/// </summary>
public static class DateTimeExtension
{
    /// <summary>
    /// Converts a nullable DateTime to a DateTimeWithFormat object.
    /// </summary>
    public static DateTimeWithFormat? GetDateTimeWithFormat(this DateTime? dateTime, string format)
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
