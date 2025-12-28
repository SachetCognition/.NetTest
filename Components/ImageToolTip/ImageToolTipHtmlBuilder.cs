using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;

public class ImageToolTipHtmlBuilder : HtmlBuilderBase<ImageToolTipComponent>
{
    public ImageToolTipHtmlBuilder(ImageToolTipComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

        var tagBuilder = new TagBuilder("img");
        tagBuilder.Attributes["id"] = Component.Id;
        tagBuilder.Attributes["src"] = Component.ImageUrl ?? string.Empty;

        if (!string.IsNullOrEmpty(Component.Title))
        {
            tagBuilder.Attributes["title"] = Component.Title;
        }

        if (!string.IsNullOrEmpty(Component.Alt))
        {
            tagBuilder.Attributes["alt"] = Component.Alt;
        }

        if (!string.IsNullOrEmpty(Component.CssClass))
        {
            tagBuilder.AddCssClass(Component.CssClass);
        }

        foreach (var attr in Component.HtmlAttributes)
        {
            tagBuilder.Attributes[attr.Key] = attr.Value?.ToString() ?? string.Empty;
        }

        tagBuilder.TagRenderMode = TagRenderMode.SelfClosing;

        using (var writer = new StringWriter())
        {
            tagBuilder.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
