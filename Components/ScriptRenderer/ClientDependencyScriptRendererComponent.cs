using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer;

public class ClientDependencyScriptRendererComponent : IScriptRendererComponent
{
    private readonly IHtmlHelper _htmlHelper;
    private readonly List<JsResource> _scripts = new();

    public ClientDependencyScriptRendererComponent(IHtmlHelper htmlHelper)
    {
        _htmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
    }

    public void RegisterScript(JsResource resource)
    {
        if (!_scripts.Any(s => s.Name == resource.Name))
        {
            _scripts.Add(resource);
        }
    }

    public IHtmlContent RenderScripts()
    {
        var sb = new StringBuilder();
        var orderedScripts = _scripts.OrderBy(s => s.Order).ToList();
        
        foreach (var script in orderedScripts)
        {
            sb.AppendLine($"<script src=\"{script.Path}\"></script>");
        }

        return new HtmlString(sb.ToString());
    }
}
