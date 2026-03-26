namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class WeekYearBuilder : ComponentBuilderBase<WeekYearComponent, WeekYearBuilder>
    {
        public WeekYearBuilder(WeekYearComponent component) : base(component) { }
        public WeekYearBuilder(WeekYearComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public WeekYearBuilder CssMainDiv(string cssClass) { return this; }
        public WeekYearBuilder DisplayInformationIcon(bool display) { return this; }
        public WeekYearBuilder AssociatedWeekYearHtmlId(string id) { return this; }
        public WeekYearBuilder CssClassWeekDiv(string cssClass) { return this; }
        public WeekYearBuilder OnWeekChange(string handler) { return this; }
    }
}
