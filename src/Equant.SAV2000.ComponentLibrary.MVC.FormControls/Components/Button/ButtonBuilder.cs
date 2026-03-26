namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Button
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ButtonBuilder : ComponentBuilderBase<ButtonComponent, ButtonBuilder>
    {
        public ButtonBuilder(ButtonComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public ButtonBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public ButtonBuilder CssClassReadOnly(string value) { Component.CssClassReadOnly = value; return this; }
        public ButtonBuilder DialogDivId(string divId) { Component.DialogDivId = divId; return this; }
        public ButtonBuilder Disabled(bool value) { Component.IsDisabled = value; return this; }
        public ButtonBuilder OnClick(string value) { Component.OnClick = value; return this; }
        public ButtonBuilder Title(string value) { Component.Title = value; return this; }
        public ButtonBuilder Value(string buttonValue) { Component.Value = buttonValue; return this; }
    }
}
