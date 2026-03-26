namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBox
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CheckBoxBuilder : ComponentBuilderBase<CheckBoxComponent, CheckBoxBuilder>
    {
        public CheckBoxBuilder(CheckBoxComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public CheckBoxBuilder IsChecked(bool value) { Component.IsChecked = value; return this; }
        public CheckBoxBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public CheckBoxBuilder CssClassDisabled(string value) { Component.CssClassDisabled = value; return this; }
        public CheckBoxBuilder OnClick(string value) { Component.OnClick = value; return this; }
        public CheckBoxBuilder OnChange(string value) { Component.OnChange = value; return this; }
        public CheckBoxBuilder Disabled(bool value) { Component.IsDisabled = value; return this; }
        public CheckBoxBuilder Title(string value) { Component.Title = value; return this; }
    }
}
