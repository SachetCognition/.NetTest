namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class WeekYearBuilder : ComponentBuilderBase<WeekYearComponent, WeekYearBuilder>
    {
        public WeekYearBuilder(WeekYearComponent component, ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public WeekYearBuilder Value(WeekYearWithFormat value)
        {
            this.Component.Value = value;
            return this;
        }

        public WeekYearBuilder Name(string name)
        {
            this.Component.Name = name;
            return this;
        }

        public WeekYearBuilder Disabled(bool disabled)
        {
            this.Component.Disabled = disabled;
            return this;
        }

        /// <summary>
        /// </summary>
        public WeekYearBuilder DisplayInformationIcon(bool display)
        {
            Component.DisplayInformationIcon = display;
            return this;
        }

        /// <summary>
        /// </summary>
        public WeekYearBuilder CssMainDiv(string cssClass)
        {
            Component.CssMainDiv = cssClass;
            return this;
        }

        /// <summary>
        /// </summary>
        public WeekYearBuilder CssClass(string cssClass)
        {
            Component.CssClass = cssClass;
            return this;
        }

        public WeekYearBuilder AssociatedWeekYearHtmlId(string value)
        {
            Component.AssociatedWeekYearHtmlId = value;
            return this;
        }
    }
}
