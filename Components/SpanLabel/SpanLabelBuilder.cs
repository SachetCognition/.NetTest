using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel;

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

    public SpanLabelBuilder For(string value)
    {
        Component.For = value;
        return this;
    }

    public SpanLabelBuilder DisplayStar(bool value)
    {
        Component.DisplayStar = value;
        return this;
    }

    public SpanLabelBuilder DisplayColon(bool value)
    {
        Component.DisplayColon = value;
        return this;
    }
}
