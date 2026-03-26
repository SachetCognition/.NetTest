namespace Equant.SAV2000.ComponentLibrary.MVC.Helpers
{
    using System;
    using System.Globalization;

    public static class WeekHelper
    {
        public static int GetIso8601WeekOfYear(DateTime date)
        {
            var cal = CultureInfo.InvariantCulture.Calendar;
            var day = cal.GetDayOfWeek(date);

            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }

            return cal.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime GetFirstDateOfWeek(int year, int weekOfYear)
        {
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            var firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            var firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }

            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }

        public static int GetWeeksInYear(int year)
        {
            var lastDay = new DateTime(year, 12, 31);
            var week = GetIso8601WeekOfYear(lastDay);
            if (week == 1)
            {
                lastDay = lastDay.AddDays(-7);
                week = GetIso8601WeekOfYear(lastDay);
            }
            return week;
        }
    }
}
