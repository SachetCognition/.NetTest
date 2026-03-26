namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate
{
    using System;
    using System.Collections.Generic;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class CompositeDateBuilder : ComponentBuilderBase<CompositeDateComponent, CompositeDateBuilder>
    {
        private readonly CustomLabelBuilder customLabelBuilder;

        public CompositeDateBuilder(CompositeDateComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
            this.customLabelBuilder = new CustomLabelBuilder(this.Component.CustomLabel, modelMetadata);
        }

        public CompositeDateBuilder CustomLabel(Action<CustomLabelBuilder> setup)
        {
            if (setup != null) setup(this.customLabelBuilder);
            return this;
        }

        public CompositeDateBuilder Value(CompositeDateViewModel value) { this.Component.ViewModel = value; return this; }
        public CompositeDateBuilder Format(string format) { this.Component.Format = format; return this; }
        public CompositeDateBuilder DisplayTime(bool value) { this.Component.DisplayTime = value; return this; }
        public CompositeDateBuilder OnDateTypeChange(string value) { this.Component.OnDateTypeChange = value; return this; }
        public CompositeDateBuilder CssMainDiv(string value) { this.Component.CssMainDiv = value; return this; }
        public CompositeDateBuilder CssClassDateDiv(string value) { this.Component.CssClassDateDiv = value; return this; }
        public CompositeDateBuilder CssClassDateInput(string value) { this.Component.CssClassDateInput = value; return this; }
        public CompositeDateBuilder CssClassDropDown(string value) { this.Component.CssClassDropDown = value; return this; }
        public CompositeDateBuilder AvailableDateTypes(List<EnumDateTypes> types) { this.Component.AvailableDateTypes = types; return this; }
    }
}
