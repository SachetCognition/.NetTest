using Microsoft.AspNetCore.Html;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

public interface IScriptRendererComponent
{
    void RegisterScript(JsResource resource);
    IHtmlContent RenderScripts();
}
