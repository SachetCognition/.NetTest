namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class WeekYearBuilder : ComponentBuilderBase<WeekYearComponent, WeekYearBuilder>
    {
        public WeekYearBuilder(WeekYearComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }
        public WeekYearBuilder DisplayInformationIcon(bool value) { this.Component.DisplayInformationIcon = value; return this; }
        public WeekYearBuilder CssMainDiv(string value) { this.Component.CssMainDiv = value; return this; }
        public WeekYearBuilder WeekTextId(string value) { this.Component.WeekTextId = value; return this; }
        public WeekYearBuilder YearTextId(string value) { this.Component.YearTextId = value; return this; }
        public WeekYearBuilder AssociatedWeekYearHtmlId(string value) { this.Component.AssociatedWeekYearHtmlId = value; return this; }
        public WeekYearBuilder CssClassWeekDiv(string value) { this.Component.CssClassWeekDiv = value; return this; }
        public WeekYearBuilder OnWeekChange(string value) { this.Component.OnWeekChange = value; return this; }
    }
}
