namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System;

    public class WeekYearWithFormatException : Exception
    {
        public WeekYearWithFormatException() { }
        public WeekYearWithFormatException(string message) : base(message) { }
        public WeekYearWithFormatException(string message, Exception innerException) : base(message, innerException) { }
    }
}
