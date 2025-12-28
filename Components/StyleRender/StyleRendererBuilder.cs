using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender;

public class StyleRendererBuilder : ComponentBuilderBase<StyleRendererComponent, StyleRendererBuilder>
{
    public StyleRendererBuilder(StyleRendererComponent component, ModelMetadata? modelMetadata)
        : base(component, modelMetadata)
    {
    }

    public StyleRendererBuilder RegisterStyle(CssResource resource)
    {
        Component.RegisterStyle(resource);
        return this;
    }

    public IHtmlContent Render()
    {
        return Component.ToHtml();
    }
}
