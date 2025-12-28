using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender;

public class ClientDependencyStyleRenderComponent : IStyleRendererComponent
{
    private readonly IHtmlHelper _htmlHelper;
    private readonly List<CssResource> _styles = new();

    public ClientDependencyStyleRenderComponent(IHtmlHelper htmlHelper)
    {
        _htmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
    }

    public void RegisterStyle(CssResource resource)
    {
        if (!_styles.Any(s => s.Name == resource.Name))
        {
            _styles.Add(resource);
        }
    }

    public IHtmlContent RenderStyles()
    {
        var sb = new StringBuilder();
        var orderedStyles = _styles.OrderBy(s => s.Order).ToList();
        
        foreach (var style in orderedStyles)
        {
            sb.AppendLine($"<link rel=\"stylesheet\" href=\"{style.Path}\" />");
        }

        return new HtmlString(sb.ToString());
    }
}
