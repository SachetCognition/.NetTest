namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System;

    public class WeekYearWithFormat
    {
        public WeekYearWithFormat() { }
        public WeekYearWithFormat(WeekFormat format) { this.Format = format; }
        public WeekFormat Format { get; set; }
        public WeekFormat WeekFormat { get; set; }
        public WeekAndYear Value { get; set; }
        public string WeekText { get; set; }
        public string YearText { get; set; }
        public double TimeOffset { get; set; }
        public bool IsUtcMode { get; set; }
        public DateTime? Date { get; set; }
        public bool IsEmpty { get { return Value == null || (Value.Week == 0 && Value.Year == 0); } }
    }
}
