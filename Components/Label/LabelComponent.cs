using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label;

public class LabelComponent : ComponentBase
{
    public LabelComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        Text = string.Empty;
    }

    public string? Text { get; set; }
    public string? CssClass { get; set; }
    public string? For { get; set; }

    public override ReadOnlyCollection<JsResource> JsResources =>
        new ReadOnlyCollection<JsResource>(new List<JsResource>());

    public override IHtmlContent ToHtml()
    {
        return new LabelHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        return string.Empty;
    }
}
