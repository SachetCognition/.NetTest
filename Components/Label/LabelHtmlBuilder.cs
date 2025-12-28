using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label;

public class LabelHtmlBuilder : HtmlBuilderBase<LabelComponent>
{
    public LabelHtmlBuilder(LabelComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

        var tagBuilder = new TagBuilder("label");
        tagBuilder.Attributes["id"] = Component.Id;

        if (!string.IsNullOrEmpty(Component.Name))
        {
            tagBuilder.Attributes["name"] = Component.Name;
        }

        if (!string.IsNullOrEmpty(Component.For))
        {
            tagBuilder.Attributes["for"] = Component.For;
        }

        if (!string.IsNullOrEmpty(Component.CssClass))
        {
            tagBuilder.AddCssClass(Component.CssClass);
        }

        foreach (var attr in Component.HtmlAttributes)
        {
            tagBuilder.Attributes[attr.Key] = attr.Value?.ToString() ?? string.Empty;
        }

        tagBuilder.InnerHtml.AppendHtml(Component.Text ?? string.Empty);

        using (var writer = new StringWriter())
        {
            tagBuilder.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
