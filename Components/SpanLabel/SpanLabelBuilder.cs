namespace Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class SpanLabelBuilder : ComponentBuilderBase<SpanLabelComponent, SpanLabelBuilder>
    {
        public SpanLabelBuilder(SpanLabelComponent component, ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public SpanLabelBuilder Text(string value)
        {
            Component.Text = value;
            return this;
        }

        public SpanLabelBuilder CssClass(string value)
        {
            Component.CssClass = value;
            return this;
        }
    }
}
