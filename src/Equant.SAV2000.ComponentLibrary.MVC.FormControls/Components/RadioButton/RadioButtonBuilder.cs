namespace Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButton
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class RadioButtonBuilder : ComponentBuilderBase<RadioButtonComponent, RadioButtonBuilder>
    {
        public RadioButtonBuilder(RadioButtonComponent component) : base(component) { }
        public RadioButtonBuilder(RadioButtonComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public RadioButtonBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public RadioButtonBuilder GroupName(string value) { Component.GroupName = value; return this; }
        public RadioButtonBuilder Value(string value) { Component.Value = value; return this; }
        public RadioButtonBuilder IsChecked(bool value) { Component.IsChecked = value; return this; }
        public RadioButtonBuilder OnChange(string value) { Component.OnChange = value; return this; }
    }
}
