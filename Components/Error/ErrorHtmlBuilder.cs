using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent;

public class ErrorHtmlBuilder : HtmlBuilderBase<ErrorComponents>
{
    public ErrorHtmlBuilder(ErrorComponents component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible || string.IsNullOrEmpty(Component.Message))
        {
            return HtmlString.Empty;
        }

        var tagBuilder = new TagBuilder("span");
        tagBuilder.Attributes["id"] = Component.Id;
        tagBuilder.AddCssClass("field-validation-error");

        if (!string.IsNullOrEmpty(Component.CssClass))
        {
            tagBuilder.AddCssClass(Component.CssClass);
        }

        foreach (var attr in Component.HtmlAttributes)
        {
            tagBuilder.Attributes[attr.Key] = attr.Value?.ToString() ?? string.Empty;
        }

        tagBuilder.InnerHtml.AppendHtml(Component.Message);

        using (var writer = new StringWriter())
        {
            tagBuilder.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
