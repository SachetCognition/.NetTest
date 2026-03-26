namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System;

    [Serializable]
    public class WeekAndYear
    {
        public int? Week { get; set; }
        public int? Year { get; set; }

        public WeekAndYear() { }

        public WeekAndYear(int? week, int? year)
        {
            this.Week = week;
            this.Year = year;
        }

        public bool IsEmpty
        {
            get { return !this.Week.HasValue && !this.Year.HasValue; }
        }
    }
}
