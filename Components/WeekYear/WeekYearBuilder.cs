namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class WeekYearBuilder : ComponentBuilderBase<WeekYearComponent, WeekYearBuilder>
    {
        public WeekYearBuilder(WeekYearComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }
        public WeekYearBuilder CssMainDiv(string value) { return this; }
        public WeekYearBuilder DisplayInformationIcon(bool value) { return this; }
        public WeekYearBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public WeekYearBuilder AssociatedWeekYearHtmlId(string value) { return this; }
        public WeekYearBuilder CssClassWeekDiv(string value) { return this; }
        public WeekYearBuilder OnWeekChange(string value) { return this; }
    }
}
