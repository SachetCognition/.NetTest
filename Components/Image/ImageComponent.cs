using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image;

public class ImageComponent : ComponentBase
{
    public ImageComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        Src = string.Empty;
        Alt = string.Empty;
    }

    public string? Src { get; set; }
    public string? Alt { get; set; }
    public string? CssClass { get; set; }
    public string? Title { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }

    public override ReadOnlyCollection<JsResource> JsResources =>
        new ReadOnlyCollection<JsResource>(
            new List<JsResource>
            {
                new JsResource(
                    "JsImage",
                    "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.Image.js",
                    200,
                    typeof(ImageComponent))
            });

    public override IHtmlContent ToHtml()
    {
        return new ImageHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        return string.Empty;
    }
}
