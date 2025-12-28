using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent;

public class ErrorComponents : ComponentBase
{
    public ErrorComponents(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        Message = string.Empty;
    }

    public string? Message { get; set; }
    public string? CssClass { get; set; }

    public override ReadOnlyCollection<JsResource> JsResources =>
        new ReadOnlyCollection<JsResource>(
            new List<JsResource>
            {
                new JsResource(
                    "JsErrorDisplay",
                    "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ErrorDisplay.js",
                    200,
                    typeof(ErrorComponents))
            });

    public override IHtmlContent ToHtml()
    {
        return new ErrorHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        return string.Empty;
    }
}
