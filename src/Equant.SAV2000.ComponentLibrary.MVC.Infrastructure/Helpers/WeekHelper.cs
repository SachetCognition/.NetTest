namespace Equant.SAV2000.ComponentLibrary.MVC.Helpers
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

    public static class WeekHelper
    {
        public static DateTime GetFirstDayOfWeek(int week, int year)
        {
            var res = new DateTime(year, 1, 4);
            res = GetFirstDayOfWeek(res);
            res = res.AddDays(7 * (week - 1));
            return res;
        }

        public static DateTime GetFirstDayOfWeek(DateTime day)
        {
            var res = new DateTime(day.Ticks);
            while (res.DayOfWeek != DayOfWeek.Monday)
            {
                res = res.AddDays(-1);
            }
            return res;
        }

        public static DateTime GetFirstDayOfWeekFromCurrentWeek(int deltaWeeks, double timeOffset, bool utcMode)
        {
            var res = DateComponentHelper.GetCurrentDate(timeOffset, utcMode);
            var x = deltaWeeks * 7;
            if (x / 7 != deltaWeeks) { x = int.MaxValue; }
            res = res.AddDays(x);
            res = GetFirstDayOfWeek(res);
            return res;
        }

        public static WeekAndYear GetWeekYearFromDateTime(DateTime day)
        {
            var res = new WeekAndYear();
            DateTime week1;
            var isoYear = day.Year;

            if (day >= new DateTime(isoYear, 12, 29))
            {
                week1 = GetFirstDayOfWeek(1, isoYear + 1);
                if (day < week1) { week1 = GetFirstDayOfWeek(1, isoYear); }
                else { isoYear++; }
            }
            else
            {
                week1 = GetFirstDayOfWeek(1, isoYear);
                if (day < week1) { week1 = GetFirstDayOfWeek(1, --isoYear); }
            }

            res.Year = isoYear;
            res.Week = ((day - week1).Days / 7 + 1);
            return res;
        }

        public static DateTime? ParseWeekAndYear(WeekYearWithFormat weekYear)
        {
            if (weekYear == null) return null;

            var week = ParseWeekString(weekYear.WeekText, weekYear.Format);

            if (week != int.MinValue)
            {
                return GetFirstDayOfWeekFromCurrentWeek(week, weekYear.TimeOffset, weekYear.IsUtcMode);
            }

            if (week == int.MinValue)
            {
                week = ParseWeekNumber(weekYear.WeekText);
            }

            if (week == int.MinValue) return null;

            var year = ParseYear(weekYear.YearText);
            if (year == int.MinValue) return null;

            return GetFirstDayOfWeek(week, year);
        }

        public static int ParseWeekString(string week, WeekFormat format)
        {
            if (string.IsNullOrEmpty(week)) return int.MinValue;

            string regexPattern = format == WeekFormat.English ? DateTimeConstants.RegexWeekEnglish : DateTimeConstants.RegexWeekFrench;
            var match = Regex.Match(week, regexPattern);

            if (!match.Success) return int.MinValue;

            return string.IsNullOrEmpty(match.Groups[1].Value) ? 0 : Convert.ToInt32(match.Groups[1].Value, CultureInfo.InvariantCulture);
        }

        public static int ParseWeekNumber(string week)
        {
            if (string.IsNullOrEmpty(week)) return int.MinValue;

            int weekParsed;
            var isInt = int.TryParse(week, out weekParsed);
            if (!isInt) return int.MinValue;
            if (weekParsed < 1 || weekParsed > 53) return int.MinValue;

            return weekParsed;
        }

        public static int ParseYear(string year)
        {
            if (string.IsNullOrEmpty(year)) return int.MinValue;

            int yearParsed;
            var isInt = int.TryParse(year, out yearParsed);
            if (!isInt) return int.MinValue;
            if (yearParsed < 1901 || yearParsed > 9999) return int.MinValue;

            return yearParsed;
        }
    }
}
