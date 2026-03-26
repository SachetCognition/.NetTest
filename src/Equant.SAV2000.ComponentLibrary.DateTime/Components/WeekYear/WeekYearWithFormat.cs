namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System;

    [Serializable]
    public class WeekYearWithFormat
    {
        public WeekAndYear Value { get; set; }
        public string Format { get; set; }

        public WeekYearWithFormat()
        {
            this.Value = new WeekAndYear();
            this.Format = "English";
        }

        public WeekYearWithFormat(string format)
        {
            this.Value = new WeekAndYear();
            this.Format = format;
        }

        public WeekYearWithFormat(int? week, int? year, string format)
        {
            this.Value = new WeekAndYear(week, year);
            this.Format = format;
        }

        public bool IsEmpty
        {
            get { return this.Value == null || this.Value.IsEmpty; }
        }
    }
}
