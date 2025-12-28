using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer;

public class ScriptRendererBuilder : ComponentBuilderBase<ScriptRendererComponent, ScriptRendererBuilder>
{
    public ScriptRendererBuilder(ScriptRendererComponent component, ModelMetadata? modelMetadata)
        : base(component, modelMetadata)
    {
    }

    public ScriptRendererBuilder RegisterScript(JsResource resource)
    {
        Component.RegisterScript(resource);
        return this;
    }

    public ScriptRendererBuilder RegisterInitScript(string script)
    {
        Component.RegisterInitScript(script);
        return this;
    }

    public IHtmlContent Render()
    {
        return Component.ToHtml();
    }
}
