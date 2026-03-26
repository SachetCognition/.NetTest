namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl
{
    using System;

    [Serializable]
    public class DateTimeWithFormatException : Exception
    {
        public DateTimeWithFormatException() { }

        public DateTimeWithFormatException(string message) : base(message) { }

        public DateTimeWithFormatException(string message, Exception inner) : base(message, inner) { }
    }
}
