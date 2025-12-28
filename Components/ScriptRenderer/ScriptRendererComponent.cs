using System.Collections.ObjectModel;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer;

public class ScriptRendererComponent : ComponentBase
{
    public const string ContextKey = "Sav2000ScriptRenderer";

    private readonly ReadOnlyCollection<IScriptRendererComponent> _renderers;
    private readonly bool _isLightRequirement;
    private readonly List<JsResource> _registeredScripts = new();
    private readonly List<string> _initScripts = new();

    public ScriptRendererComponent(
        IHtmlHelper htmlHelper,
        ReadOnlyCollection<IScriptRendererComponent> renderers,
        bool isLightRequirement = false)
        : base(htmlHelper)
    {
        _renderers = renderers ?? throw new ArgumentNullException(nameof(renderers));
        _isLightRequirement = isLightRequirement;
    }

    public bool IsLightRequirement => _isLightRequirement;

    public void RegisterScript(JsResource resource)
    {
        if (!_registeredScripts.Any(r => r.Name == resource.Name))
        {
            _registeredScripts.Add(resource);
        }
    }

    public void RegisterInitScript(string script)
    {
        _initScripts.Add(script);
    }

    public override IHtmlContent ToHtml()
    {
        var sb = new StringBuilder();
        
        var orderedScripts = _registeredScripts.OrderBy(s => s.Order).ToList();
        foreach (var script in orderedScripts)
        {
            sb.AppendLine($"<script src=\"{script.Path}\"></script>");
        }

        if (_initScripts.Count > 0)
        {
            sb.AppendLine("<script>");
            sb.AppendLine("$(document).ready(function() {");
            foreach (var initScript in _initScripts)
            {
                sb.AppendLine(initScript);
            }
            sb.AppendLine("});");
            sb.AppendLine("</script>");
        }

        return new HtmlString(sb.ToString());
    }

    public override string ToInitScript()
    {
        return string.Empty;
    }
}
