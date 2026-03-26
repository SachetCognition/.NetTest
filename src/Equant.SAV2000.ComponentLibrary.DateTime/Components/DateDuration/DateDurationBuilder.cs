namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateDuration
{
    using System;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class DateDurationBuilder : ComponentBuilderBase<DateDurationComponent, DateDurationBuilder>
    {
        private readonly CustomLabelBuilder customLabelBuilder;

        public DateDurationBuilder(DateDurationComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
            this.customLabelBuilder = new CustomLabelBuilder(this.Component.CustomLabel, modelMetadata);
        }

        public DateDurationBuilder CustomLabel(Action<CustomLabelBuilder> setup)
        {
            if (setup != null) setup(this.customLabelBuilder);
            return this;
        }

        public DateDurationBuilder Value(DateDuration value) { this.Component.Value = value; return this; }
        public DateDurationBuilder Format(string format) { this.Component.Format = format; return this; }
        public DateDurationBuilder DisplayTime(bool value) { this.Component.DisplayTime = value; return this; }
        public DateDurationBuilder CssMainDiv(string value) { this.Component.CssMainDiv = value; return this; }
        public DateDurationBuilder CssClassDateInput(string value) { this.Component.CssClassDateInput = value; return this; }
        public DateDurationBuilder CssClassDurationInput(string value) { this.Component.CssClassDurationInput = value; return this; }
    }
}
