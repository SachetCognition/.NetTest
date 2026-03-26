namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class WeekYearBuilder : ComponentBuilderBase<WeekYearComponent, WeekYearBuilder>
    {
        private readonly CustomLabelBuilder customLabelBuilder;

        public WeekYearBuilder(WeekYearComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
            this.customLabelBuilder = new CustomLabelBuilder(this.Component.CustomLabel, modelMetadata);
        }

        public WeekYearBuilder CustomLabel(Action<CustomLabelBuilder> setup)
        {
            if (setup != null) setup(this.customLabelBuilder);
            return this;
        }

        public WeekYearBuilder Value(WeekYearWithFormat value) { this.Component.Value = value; return this; }
        public WeekYearBuilder CssMainDiv(string value) { this.Component.CssMainDiv = value; return this; }
        public WeekYearBuilder CssClassWeekInput(string value) { this.Component.CssClassWeekInput = value; return this; }
        public WeekYearBuilder CssClassYearInput(string value) { this.Component.CssClassYearInput = value; return this; }
        public WeekYearBuilder OnWeekChange(string value) { this.Component.OnWeekChange = value; return this; }
    }
}
