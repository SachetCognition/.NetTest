using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink;

public class HyperLinkHtmlBuilder : HtmlBuilderBase<HyperLinkComponent>
{
    public HyperLinkHtmlBuilder(HyperLinkComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

        var tagBuilder = new TagBuilder("a");
        tagBuilder.Attributes["id"] = Component.Id;
        tagBuilder.Attributes["href"] = Component.Href ?? "#";

        if (!string.IsNullOrEmpty(Component.Name))
        {
            tagBuilder.Attributes["name"] = Component.Name;
        }

        if (!string.IsNullOrEmpty(Component.Title))
        {
            tagBuilder.Attributes["title"] = Component.Title;
        }

        if (!string.IsNullOrEmpty(Component.Target))
        {
            tagBuilder.Attributes["target"] = Component.Target;
        }

        var cssClass = Component.IsDisabled ? Component.CssClassReadOnly : Component.CssClass;
        if (!string.IsNullOrEmpty(cssClass))
        {
            tagBuilder.AddCssClass(cssClass);
        }

        foreach (var attr in Component.HtmlAttributes)
        {
            tagBuilder.Attributes[attr.Key] = attr.Value?.ToString() ?? string.Empty;
        }

        if (!string.IsNullOrEmpty(Component.ImageUrl))
        {
            var img = new TagBuilder("img");
            img.Attributes["src"] = Component.ImageUrl;
            if (!string.IsNullOrEmpty(Component.ImageAlt))
            {
                img.Attributes["alt"] = Component.ImageAlt;
            }
            img.TagRenderMode = TagRenderMode.SelfClosing;
            tagBuilder.InnerHtml.AppendHtml(img);
        }
        else
        {
            tagBuilder.InnerHtml.AppendHtml(Component.Text ?? string.Empty);
        }

        using (var writer = new StringWriter())
        {
            tagBuilder.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
