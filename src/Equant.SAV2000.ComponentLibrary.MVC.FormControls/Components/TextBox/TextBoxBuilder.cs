namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextBox
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class TextBoxBuilder : ComponentBuilderBase<TextBoxComponent, TextBoxBuilder>
    {
        public TextBoxBuilder(TextBoxComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public TextBoxBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public TextBoxBuilder CssClassReadOnly(string value) { Component.CssClassReadOnly = value; return this; }
        public TextBoxBuilder Value(string value) { Component.Value = value; return this; }
        public TextBoxBuilder ReadOnly(bool value) { Component.IsReadOnly = value; return this; }
        public TextBoxBuilder Disabled(bool value) { Component.IsDisabled = value; return this; }
        public TextBoxBuilder MaxLength(int value) { Component.MaxLength = value; return this; }
        public TextBoxBuilder Placeholder(string value) { Component.Placeholder = value; return this; }
    }
}
