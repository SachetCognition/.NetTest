using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ActionButton;

/// <summary>
/// The component class for the ActionButton component.
/// </summary>
public class ActionButtonComponent : ComponentBase
{
    public ActionButtonComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        OnClick = "null";
        Title = string.Empty;
        Value = string.Empty;
    }

    public override ReadOnlyCollection<JsResource> JsResources =>
        new ReadOnlyCollection<JsResource>(
            new List<JsResource>
            {
                new JsResource(
                    "JsActionButton",
                    "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ActionButton.js",
                    200,
                    typeof(ActionButtonComponent))
            });

    public string? CssClass { get; set; }

    public string? DialogBoxId { get; set; }

    public string? CssClassReadOnly { get; set; }

    public string? CssSpan { get; set; }

    public string OnClick { get; set; }

    public string? AccessText { get; set; }

    public bool IsDisabled
    {
        get => HtmlAttributes.ContainsKey("disabled");
        set
        {
            if (value)
            {
                HtmlAttributes["disabled"] = "disabled";
            }
            else
            {
                if (HtmlAttributes.ContainsKey("disabled"))
                {
                    HtmlAttributes.Remove("disabled");
                }
            }
        }
    }

    public string? Title
    {
        get => HtmlAttributes.TryGetValue("title", out var title) ? title?.ToString() : null;
        set
        {
            if (value != null)
            {
                HtmlAttributes["title"] = value;
            }
        }
    }

    public string? Value
    {
        get
        {
            if (!HtmlAttributes.ContainsKey("value") ||
               string.IsNullOrEmpty(HtmlAttributes["value"]?.ToString()))
            {
                throw new ArgumentException("The attribute 'value' is mandatory for submit button");
            }

            return HtmlAttributes.TryGetValue("value", out var val) ? val?.ToString() : null;
        }
        set => HtmlAttributes["value"] = value!;
    }

    public string? Text { get; set; }

    public override IHtmlContent ToHtml()
    {
        return new ActionButtonHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        var options = JsonConvert.SerializeObject(new { onClick = new JRaw(OnClick) });
        return $"$('#{Id}').actionButton({options});";
    }
}
