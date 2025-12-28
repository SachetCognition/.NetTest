using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;

public class ImageToolTipComponent : ComponentBase
{
    public ImageToolTipComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        Text = string.Empty;
        ImageUrl = string.Empty;
    }

    public string? Text { get; set; }
    public string? ImageUrl { get; set; }
    public string? CssClass { get; set; }
    public string? ToolTipId { get; set; }
    public string? Title { get; set; }
    public string? Alt { get; set; }

    public override ReadOnlyCollection<JsResource> JsResources =>
        new ReadOnlyCollection<JsResource>(
            new List<JsResource>
            {
                new JsResource(
                    "JsImageToolTip",
                    "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ImageToolTip.js",
                    200,
                    typeof(ImageToolTipComponent))
            });

    public override IHtmlContent ToHtml()
    {
        return new ImageToolTipHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        var options = JsonConvert.SerializeObject(new { toolTipId = ToolTipId, text = Text });
        return $"$('#{Id}').imageToolTip({options});";
    }
}
