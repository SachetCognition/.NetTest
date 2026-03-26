namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Duration
{
    using System;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class DurationBuilder : ComponentBuilderBase<DurationComponent, DurationBuilder>
    {
        private readonly CustomLabelBuilder customLabelBuilder;

        public DurationBuilder(DurationComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
            this.customLabelBuilder = new CustomLabelBuilder(this.Component.CustomLabel, modelMetadata);
        }

        public DurationBuilder CustomLabel(Action<CustomLabelBuilder> setup)
        {
            if (setup != null) setup(this.customLabelBuilder);
            return this;
        }

        public DurationBuilder Value(DurationEntity value) { this.Component.Value = value; return this; }
        public DurationBuilder CssMainDiv(string value) { this.Component.CssMainDiv = value; return this; }
        public DurationBuilder CssClassInput(string value) { this.Component.CssClassInput = value; return this; }
        public DurationBuilder DisplaySeconds(bool value) { this.Component.DisplaySeconds = value; return this; }
        public DurationBuilder DisplayMilliseconds(bool value) { this.Component.DisplayMilliseconds = value; return this; }
    }
}
