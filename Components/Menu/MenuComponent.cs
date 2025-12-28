using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;

public class MenuComponent : ComponentBase
{
    public MenuComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        Items = new List<MenuItem>();
    }

    public List<MenuItem> Items { get; set; }
    public string? CssClass { get; set; }
    public string? CssClassItem { get; set; }
    public string? CssClassSelected { get; set; }

    public override ReadOnlyCollection<JsResource> JsResources =>
        new ReadOnlyCollection<JsResource>(new List<JsResource>());

    public override IHtmlContent ToHtml()
    {
        return new MenuHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        return string.Empty;
    }
}
