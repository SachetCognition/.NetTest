namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ActionButton
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ActionButtonBuilder : ComponentBuilderBase<ActionButtonComponent, ActionButtonBuilder>
    {
        public ActionButtonBuilder(ActionButtonComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public ActionButtonBuilder CssClass(string css) { Component.CssClass = css; return this; }
        public ActionButtonBuilder CssClassReadOnly(string css) { Component.CssClassReadOnly = css; return this; }
        public ActionButtonBuilder CssSpan(string css) { Component.CssSpan = css; return this; }
        public ActionButtonBuilder DialogBoxId(string divId) { Component.DialogBoxId = divId; return this; }
        public ActionButtonBuilder Disabled(bool isDisabled) { Component.IsDisabled = isDisabled; return this; }
        public ActionButtonBuilder OnClick(string value) { Component.OnClick = value; return this; }
        public ActionButtonBuilder Title(string value) { Component.Title = value; return this; }
        public ActionButtonBuilder Value(string val) { Component.Value = val; return this; }
        public ActionButtonBuilder Text(string value) { Component.Text = value; return this; }
        public ActionButtonBuilder AccessText(string text) { Component.AccessText = text; return this; }
    }
}
