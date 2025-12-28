using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel;

public class SpanLabelHtmlBuilder : HtmlBuilderBase<SpanLabelComponent>
{
    public SpanLabelHtmlBuilder(SpanLabelComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

        var tagBuilder = new TagBuilder("span");
        tagBuilder.Attributes["id"] = Component.Id;

        if (!string.IsNullOrEmpty(Component.CssClass))
        {
            tagBuilder.AddCssClass(Component.CssClass);
        }

        foreach (var attr in Component.HtmlAttributes)
        {
            tagBuilder.Attributes[attr.Key] = attr.Value?.ToString() ?? string.Empty;
        }

        if (Component.DisplayStar)
        {
            var starSpan = new TagBuilder("span");
            starSpan.AddCssClass("mandatory-star");
            starSpan.InnerHtml.AppendHtml("*");
            tagBuilder.InnerHtml.AppendHtml(starSpan);
        }

        tagBuilder.InnerHtml.AppendHtml(Component.Text ?? string.Empty);

        if (Component.DisplayColon)
        {
            tagBuilder.InnerHtml.AppendHtml(":");
        }

        using (var writer = new StringWriter())
        {
            tagBuilder.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
