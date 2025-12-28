using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;

/// <summary>
/// Control which generates a span or label composed of:
/// - the main label
/// - a superscript text
/// - an asterisk (star) if needed
/// - a semicolon if needed
/// </summary>
public class CustomLabelComponent : ComponentBase
{
    public CustomLabelComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        DisplayColon = true;
        DisplayStar = false;
        IsHtmlEncode = false;
        IsOnlyForAccess = false;
    }

    public string AssociatedControlId
    {
        get => HtmlAttributes.TryGetValue("for", out var val) ? val?.ToString() ?? string.Empty : string.Empty;
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                HtmlAttributes["for"] = value;
            }
        }
    }

    public string? CssClassLabel { get; set; }

    public bool IsOnlyForAccess { get; set; }

    public bool DisplayColon { get; set; }

    public bool DisplayStar { get; set; }

    public bool IsHtmlEncode { get; set; }

    public string? Text { get; set; }

    public string? SuperscriptText { get; set; }

    public string? SuperscriptCssClass { get; set; }

    public string? SuperscriptToolTip { get; set; }

    public string? AccessText { get; set; }

    public override string Id { get; set; } = string.Empty;

    public override IHtmlContent ToHtml()
    {
        return new CustomLabelHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        return string.Empty;
    }
}
