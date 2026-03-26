namespace Equant.SAV2000.ComponentLibrary.MVC.Extensions
{
    using System;

    public static class DateTimeExtension
    {
        public static string ToFormattedString(this DateTime dateTime, string format)
        {
            return dateTime.ToString(format);
        }
    }
}
