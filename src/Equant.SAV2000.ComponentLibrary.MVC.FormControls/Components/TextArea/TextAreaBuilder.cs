namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextArea
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class TextAreaBuilder : ComponentBuilderBase<TextAreaComponent, TextAreaBuilder>
    {
        public TextAreaBuilder(TextAreaComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public TextAreaBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public TextAreaBuilder Value(string value) { Component.Value = value; return this; }
        public TextAreaBuilder Rows(int value) { Component.Rows = value; return this; }
        public TextAreaBuilder Cols(int value) { Component.Cols = value; return this; }
        public TextAreaBuilder MaxLength(int value) { Component.MaxLength = value; return this; }
        public TextAreaBuilder ReadOnly(bool value) { Component.IsReadOnly = value; return this; }
        public TextAreaBuilder Disabled(bool value) { Component.IsDisabled = value; return this; }
    }
}
