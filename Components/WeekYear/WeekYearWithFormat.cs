namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System;

    public class WeekYearWithFormat
    {
        public WeekYearWithFormat() { }
        public WeekYearWithFormat(string format) { this.Format = format; }
        public string Format { get; set; }
        public WeekAndYear Value { get; set; }
        public string WeekText { get; set; }
        public string YearText { get; set; }
        public double TimeOffset { get; set; }
        public bool IsUtcMode { get; set; }
        public WeekFormat WeekFormat { get; set; }
        public DateTime? Date { get; set; }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(this.WeekText) && string.IsNullOrEmpty(this.YearText);
            }
        }
    }
}
