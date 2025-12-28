using System.Collections.ObjectModel;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender;

public class StyleRendererComponent : ComponentBase
{
    public const string ContextKey = "Sav2000StyleRenderer";

    private readonly ReadOnlyCollection<IStyleRendererComponent> _renderers;
    private readonly List<CssResource> _registeredStyles = new();

    public StyleRendererComponent(
        IHtmlHelper htmlHelper,
        ReadOnlyCollection<IStyleRendererComponent> renderers)
        : base(htmlHelper)
    {
        _renderers = renderers ?? throw new ArgumentNullException(nameof(renderers));
    }

    public void RegisterStyle(CssResource resource)
    {
        if (!_registeredStyles.Any(r => r.Name == resource.Name))
        {
            _registeredStyles.Add(resource);
        }
    }

    public override IHtmlContent ToHtml()
    {
        var sb = new StringBuilder();
        
        var orderedStyles = _registeredStyles.OrderBy(s => s.Order).ToList();
        foreach (var style in orderedStyles)
        {
            sb.AppendLine($"<link rel=\"stylesheet\" href=\"{style.Path}\" />");
        }

        return new HtmlString(sb.ToString());
    }

    public override string ToInitScript()
    {
        return string.Empty;
    }
}
