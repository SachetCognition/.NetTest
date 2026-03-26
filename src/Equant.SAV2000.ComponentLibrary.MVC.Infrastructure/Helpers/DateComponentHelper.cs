namespace Equant.SAV2000.ComponentLibrary.MVC.Helpers
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

    public static class DateComponentHelper
    {
        public static DateTime ParseModelDate(string modelDate, string format, double? timeOffset, bool utcMode)
        {
            if (!CheckValidFormat(format))
            {
                return DateTime.MinValue;
            }

            string regexPattern = format == DateTimeConstants.EnglishFormat ? "^[D|d]{1}(([+-]{1}[0-999]{1,3})?)$" : "^[J|j]{1}(([+-]{1}[0-999]{1,3})?)$";

            var match = Regex.Match(modelDate, regexPattern);

            if (!match.Success)
            {
                return DateTime.MinValue;
            }

            var parsedDate = GetCurrentDate(timeOffset, utcMode);

            if (!String.IsNullOrEmpty(match.Groups[1].Value))
            {
                parsedDate = parsedDate.AddDays(Convert.ToInt32(match.Groups[1].Value, CultureInfo.InvariantCulture));
            }

            return parsedDate;
        }

        public static bool CheckValidFormat(string format)
        {
            if (format != DateTimeConstants.FrenchFormat && format != DateTimeConstants.EnglishFormat)
            {
                return false;
            }
            return true;
        }

        public static double ResolveOffset(double? timeOffset, bool utcMode)
        {
            var timeOffsetParsed = timeOffset.HasValue ? timeOffset.Value : 1;

            if (utcMode || (Math.Abs(timeOffsetParsed) < Double.Epsilon))
            {
                return -TimeZoneInfo.Local.BaseUtcOffset.TotalMilliseconds;
            }

            if ((Math.Abs(timeOffsetParsed - 1.0) < Double.Epsilon))
            {
                return 0;
            }

            return timeOffsetParsed * 60 * 1000 - TimeZoneInfo.Local.BaseUtcOffset.TotalMilliseconds;
        }

        public static double ResolveOffset(double? timeOffset)
        {
            if (timeOffset.HasValue)
            {
                return timeOffset.Value;
            }
            return 1;
        }

        public static DateTime GetCurrentDate(double timeOffset)
        {
            var offsetDateTime = DateTime.Now.AddMilliseconds(timeOffset);
            var currentDate = new DateTime(offsetDateTime.Year, offsetDateTime.Month, offsetDateTime.Day);
            return currentDate;
        }

        public static DateTime GetCurrentDate(double? timeOffset, bool utcMode)
        {
            var offset = ResolveOffset(timeOffset, utcMode);
            return GetCurrentDate(offset);
        }
    }
}
