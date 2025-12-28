using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label;

public class LabelBuilder : ComponentBuilderBase<LabelComponent, LabelBuilder>
{
    public LabelBuilder(LabelComponent component, ModelMetadata? modelMetadata)
        : base(component, modelMetadata)
    {
    }

    public LabelBuilder Text(string value)
    {
        Component.Text = value;
        return this;
    }

    public LabelBuilder CssClass(string value)
    {
        Component.CssClass = value;
        return this;
    }

    public LabelBuilder For(string value)
    {
        Component.For = value;
        return this;
    }
}
