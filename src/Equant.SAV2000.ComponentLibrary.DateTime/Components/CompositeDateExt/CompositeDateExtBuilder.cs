namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDateExt
{
    using System;
    using System.Collections.Generic;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class CompositeDateExtBuilder : ComponentBuilderBase<CompositeDateExtComponent, CompositeDateExtBuilder>
    {
        private readonly CustomLabelBuilder customLabelBuilder;

        public CompositeDateExtBuilder(CompositeDateExtComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
            this.customLabelBuilder = new CustomLabelBuilder(this.Component.CustomLabel, modelMetadata);
        }

        public CompositeDateExtBuilder CustomLabel(Action<CustomLabelBuilder> setup)
        {
            if (setup != null) setup(this.customLabelBuilder);
            return this;
        }

        public CompositeDateExtBuilder Value(CompositeDateExtViewModel value) { this.Component.ViewModel = value; return this; }
        public CompositeDateExtBuilder Format(string format) { this.Component.Format = format; return this; }
        public CompositeDateExtBuilder DisplayTime(bool value) { this.Component.DisplayTime = value; return this; }
        public CompositeDateExtBuilder OnDateTypeChange(string value) { this.Component.OnDateTypeChange = value; return this; }
        public CompositeDateExtBuilder CssMainDiv(string value) { this.Component.CssMainDiv = value; return this; }
        public CompositeDateExtBuilder CssClassDateDiv(string value) { this.Component.CssClassDateDiv = value; return this; }
        public CompositeDateExtBuilder CssClassDateInput(string value) { this.Component.CssClassDateInput = value; return this; }
        public CompositeDateExtBuilder CssClassDropDown(string value) { this.Component.CssClassDropDown = value; return this; }
        public CompositeDateExtBuilder AvailableDateTypes(List<EnumDateTypes> types) { this.Component.AvailableDateTypes = types; return this; }
    }
}
