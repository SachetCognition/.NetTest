namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;

    public class WeekYearWithFormat
    {
        public string Week { get; set; }
        public string Year { get; set; }
        public string WeekText { get; set; }
        public string YearText { get; set; }
        public WeekFormat Format { get; set; }
        public double TimeOffset { get; set; }
        public bool IsUtcMode { get; set; }
        public System.DateTime? Date { get; set; }
        public bool IsEmpty { get { return string.IsNullOrEmpty(Week) && string.IsNullOrEmpty(Year) && string.IsNullOrEmpty(WeekText) && string.IsNullOrEmpty(YearText); } }
    }
}
