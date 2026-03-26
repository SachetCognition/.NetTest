namespace Equant.SAV2000.ComponentLibrary.MVC.Components.PopOver
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class PopOverBuilder : ComponentBuilderBase<PopOverComponent, PopOverBuilder>
    {
        public PopOverBuilder(PopOverComponent component) : base(component) { }
        public PopOverBuilder(PopOverComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public PopOverBuilder Text(string text) { Component.Text = text; return this; }
        public PopOverBuilder Title(string title) { Component.Title = title; return this; }
        public PopOverBuilder CssClass(string cssClass) { Component.CssClass = cssClass; return this; }
        public PopOverBuilder TriggerElement(string element) { Component.TriggerElement = element; return this; }
        public PopOverBuilder Placement(string placement) { Component.Placement = placement; return this; }
    }
}
