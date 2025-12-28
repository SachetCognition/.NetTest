using System.IO;
using System.Net;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;

/// <summary>
/// The label helper.
/// </summary>
public static class LabelHelper
{
    public static string InnerSpanTag(string spanText, string spanCssClass, string? spanToolTip, bool isHtmlEncode)
    {
        var tagBuilderSpan = new TagBuilder("span");

        if (!string.IsNullOrEmpty(spanToolTip))
        {
            tagBuilderSpan.Attributes["title"] = spanToolTip;
        }

        if (!string.IsNullOrEmpty(spanCssClass))
        {
            tagBuilderSpan.Attributes["class"] = spanCssClass;
        }

        if (isHtmlEncode)
        {
            tagBuilderSpan.InnerHtml.Append(spanText);
        }
        else
        {
            tagBuilderSpan.InnerHtml.AppendHtml(spanText);
        }

        using (var writer = new StringWriter())
        {
            tagBuilderSpan.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }

    public static string InnerAbbrTag(string abbrText, string spanToolTip)
    {
        var tagBuilderSpan = new TagBuilder("abbr");

        if (!string.IsNullOrEmpty(spanToolTip))
        {
            tagBuilderSpan.Attributes["title"] = spanToolTip;
        }

        tagBuilderSpan.Attributes["class"] = "required";
        tagBuilderSpan.InnerHtml.AppendHtml(abbrText);

        using (var writer = new StringWriter())
        {
            tagBuilderSpan.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}
