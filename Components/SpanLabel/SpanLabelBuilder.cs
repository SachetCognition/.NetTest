namespace Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class SpanLabelBuilder : ComponentBuilderBase<SpanLabelComponent, SpanLabelBuilder>
    {
        public SpanLabelBuilder(SpanLabelComponent component) : base(component) { }
        public SpanLabelBuilder(SpanLabelComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public SpanLabelBuilder Text(string text) { Component.Text = text; return this; }
    }
}
