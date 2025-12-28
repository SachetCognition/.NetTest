using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink;

public class HyperLinkComponent : ComponentBase
{
    public HyperLinkComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        Text = string.Empty;
        Href = "#";
        OnClick = "null";
    }

    public string? Text { get; set; }
    public string? Href { get; set; }
    public string? CssClass { get; set; }
    public string? CssClassReadOnly { get; set; }
    public string? OnClick { get; set; }
    public string? Title { get; set; }
    public string? Target { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImageAlt { get; set; }

    public override ReadOnlyCollection<JsResource> JsResources =>
        new ReadOnlyCollection<JsResource>(
            new List<JsResource>
            {
                new JsResource(
                    "JsHyperLink",
                    "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.HyperLink.js",
                    200,
                    typeof(HyperLinkComponent))
            });

    public override IHtmlContent ToHtml()
    {
        return new HyperLinkHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        var options = JsonConvert.SerializeObject(new { onClick = new JRaw(OnClick) });
        return $"$('#{Id}').hyperLink({options});";
    }
}
