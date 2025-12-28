using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Html;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

public interface IComponent
{
    string Id { get; set; }
    string? Name { get; set; }
    bool IsVisible { get; set; }
    IDictionary<string, object> HtmlAttributes { get; }
    ReadOnlyCollection<JsResource> JsResources { get; }
    ReadOnlyCollection<CssResource> CssResources { get; }
    IHtmlContent ToHtml();
    string ToInitScript();
}
