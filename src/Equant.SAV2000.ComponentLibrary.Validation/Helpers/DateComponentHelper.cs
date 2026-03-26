namespace Equant.SAV2000.ComponentLibrary.MVC.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

    public static class DateComponentHelper
    {
        public static string[] WeekDateTypesDouble()
        {
            var weekDateTypes = new[] { EnumDateTypes.WeekBetween.ToString() };
            return weekDateTypes;
        }

        public static HashSet<string> StandardDateTypes()
        {
            var standardDateTypes = new HashSet<string>
            {
                EnumDateTypes.Empty.ToString(),
                EnumDateTypes.Between.ToString(),
                EnumDateTypes.Equal.ToString(),
                EnumDateTypes.GreaterThan.ToString(),
                EnumDateTypes.GreaternThanOrEqual.ToString(),
                EnumDateTypes.LessThan.ToString(),
                EnumDateTypes.LessThanOrEqual.ToString(),
                EnumDateTypes.EqualCurrentDate.ToString(),
                EnumDateTypes.Between.ToString()
            };
            return standardDateTypes;
        }

        public static HashSet<string> ModelDateTypes()
        {
            var modelDateTypes = new HashSet<string>
            {
                EnumDateTypes.ModelBetween.ToString(),
                EnumDateTypes.ModelEqual.ToString(),
                EnumDateTypes.ModelGreaterThan.ToString(),
                EnumDateTypes.ModelGreaterThanOrEqual.ToString(),
                EnumDateTypes.ModelLessThan.ToString(),
                EnumDateTypes.ModelLessThanOrEqual.ToString()
            };
            return modelDateTypes;
        }

        public static string[] GetDateTypesDouble()
        {
            var dateTypes = new[] { EnumDateTypes.Between.ToString(), EnumDateTypes.ModelBetween.ToString() };
            return dateTypes;
        }

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

        public static double ResolveOffset(DateTimeWithFormat dateTimeWithFormat)
        {
            if (dateTimeWithFormat == null)
            {
                throw new ArgumentException("Provided dateTimeWithFormat cannot be null");
            }

            return ResolveOffset(dateTimeWithFormat.TimeOffset, dateTimeWithFormat.IsUtcMode);
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

        public static DateTime GetCurrentDate(DateTimeWithFormat dateTimeWithFormat)
        {
            var offSet = ResolveOffset(dateTimeWithFormat);
            return GetCurrentDate(offSet);
        }

        public static DateTime GetCurrentDate(double? timeOffset, bool utcMode)
        {
            var offset = ResolveOffset(timeOffset, utcMode);
            return GetCurrentDate(offset);
        }
    }
}
