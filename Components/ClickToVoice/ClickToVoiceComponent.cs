using System.Collections.ObjectModel;
using System.Net;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ClickToVoice;

public class ClickToVoiceComponent : ComponentBase
{
    public ClickToVoiceComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        Title = string.Empty;
        TelephoneNumber = string.Empty;
        ActionUrl = string.Empty;
        ImageUrl = string.Empty;
        AlternateText = string.Empty;
    }

    public override ReadOnlyCollection<JsResource> JsResources =>
        new ReadOnlyCollection<JsResource>(
            new List<JsResource>
            {
                new JsResource(
                    "jsQtip",
                    "Equant.SAV2000.ComponentLibrary.Common.Resources.Javascripts.jquery.qtip.js",
                    200,
                    typeof(ClickToVoiceComponent)),
                new JsResource(
                    "JsClickToVoice",
                    "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ClickToVoice.js",
                    200,
                    typeof(ClickToVoiceComponent))
            });

    public override ReadOnlyCollection<CssResource> CssResources =>
        new ReadOnlyCollection<CssResource>(
            new List<CssResource>
            {
                new CssResource(
                    "Cssjqueryqtip",
                    "Equant.SAV2000.ComponentLibrary.Common.Resources.Css.jquery.qtip.css",
                    200,
                    typeof(ClickToVoiceComponent))
            });

    public string? CssAnchorTag { get; set; }
    public string? CssClassImage { get; set; }
    public string? CssTelephoneNumberSpan { get; set; }
    public string? Title { get; set; }
    public string? ActionUrl { get; set; }
    public string? ImageUrl { get; set; }
    public string? AlternateText { get; set; }
    public string? TelephoneNumber { get; set; }
    public bool IsNumberSpanVisible { get; set; }
    public string? TelephoneControlId { get; set; }
    public string? RootService { get; set; }

    public override IHtmlContent ToHtml()
    {
        return new ClickToVoiceHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        if (!string.IsNullOrEmpty(RootService))
        {
            ActionUrl = ActionUrl?.Replace("{1}", WebUtility.UrlEncode(RootService));
        }

        var options = JsonConvert.SerializeObject(new
        {
            id = Id,
            url = ActionUrl,
            telephoneNumber = TelephoneNumber,
            telephoneControlId = TelephoneControlId,
            title = "Click to call"
        });
        return $"$('#{Id}').clickToVoice({options});";
    }
}
