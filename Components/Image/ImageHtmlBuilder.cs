using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image;

public class ImageHtmlBuilder : HtmlBuilderBase<ImageComponent>
{
    public ImageHtmlBuilder(ImageComponent component)
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
        tagBuilder.Attributes["src"] = Component.Src ?? string.Empty;
        tagBuilder.Attributes["alt"] = Component.Alt ?? string.Empty;

        if (!string.IsNullOrEmpty(Component.Title))
        {
            tagBuilder.Attributes["title"] = Component.Title;
        }

        if (!string.IsNullOrEmpty(Component.CssClass))
        {
            tagBuilder.AddCssClass(Component.CssClass);
        }

        if (Component.Width.HasValue)
        {
            tagBuilder.Attributes["width"] = Component.Width.Value.ToString();
        }

        if (Component.Height.HasValue)
        {
            tagBuilder.Attributes["height"] = Component.Height.Value.ToString();
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
