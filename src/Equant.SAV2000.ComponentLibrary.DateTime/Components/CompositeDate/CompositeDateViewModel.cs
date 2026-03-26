namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;

    public class CompositeDateViewModel
    {
        public EnumDateTypes SelectedDateType { get; set; }
        public DateTimeWithFormat DateFrom { get; set; }
        public DateTimeWithFormat DateTo { get; set; }
        public string WeekFrom { get; set; }
        public string WeekTo { get; set; }
        public string YearFrom { get; set; }
        public string YearTo { get; set; }
        public string ModelValue { get; set; }

        public CompositeDateViewModel()
        {
            this.SelectedDateType = EnumDateTypes.Between;
            this.WeekFrom = string.Empty;
            this.WeekTo = string.Empty;
            this.YearFrom = string.Empty;
            this.YearTo = string.Empty;
            this.ModelValue = string.Empty;
        }
    }
}
