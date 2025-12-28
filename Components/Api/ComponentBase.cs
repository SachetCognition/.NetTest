using System.Collections.ObjectModel;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

public abstract class ComponentBase : IComponent
{
    private readonly IHtmlHelper _htmlHelper;

    protected ComponentBase(IHtmlHelper htmlHelper)
    {
        _htmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
        HtmlAttributes = new Dictionary<string, object>();
        IsVisible = true;
        Id = string.Empty;
    }

    public IHtmlHelper HtmlHelper => _htmlHelper;

    public string Id { get; set; }

    public string? Name { get; set; }

    public bool IsVisible { get; set; }

    public IDictionary<string, object> HtmlAttributes { get; }

    public virtual ReadOnlyCollection<JsResource> JsResources => new ReadOnlyCollection<JsResource>(new List<JsResource>());

    public virtual ReadOnlyCollection<CssResource> CssResources => new ReadOnlyCollection<CssResource>(new List<CssResource>());

    public abstract IHtmlContent ToHtml();

    public abstract string ToInitScript();

    protected virtual void WriteHtml(StringBuilder writer)
    {
    }

    protected virtual void WriteInitScript(StringBuilder writer)
    {
    }
}
