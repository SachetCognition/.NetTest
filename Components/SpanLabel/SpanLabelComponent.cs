using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel;

public class SpanLabelComponent : ComponentBase
{
    public SpanLabelComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        Text = string.Empty;
    }

    public string? Text { get; set; }
    public string? CssClass { get; set; }
    public string? For { get; set; }
    public bool DisplayStar { get; set; }
    public bool DisplayColon { get; set; }

    public override ReadOnlyCollection<JsResource> JsResources =>
        new ReadOnlyCollection<JsResource>(new List<JsResource>());

    public override IHtmlContent ToHtml()
    {
        return new SpanLabelHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        return string.Empty;
    }
}
