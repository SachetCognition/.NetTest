using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;

public class DropDownListHtmlBuilder : HtmlBuilderBase<DropDownListComponent>
{
    public DropDownListHtmlBuilder(DropDownListComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

        var tagBuilder = new TagBuilder("select");
        tagBuilder.Attributes["id"] = Component.Id;

        if (!string.IsNullOrEmpty(Component.Name))
        {
            tagBuilder.Attributes["name"] = Component.Name;
        }

        if (!string.IsNullOrEmpty(Component.Title))
        {
            tagBuilder.Attributes["title"] = Component.Title;
        }

        var cssClass = Component.IsDisabled ? Component.CssClassReadOnly : Component.CssClass;
        if (!string.IsNullOrEmpty(cssClass))
        {
            tagBuilder.AddCssClass(cssClass);
        }

        if (Component.IsDisabled)
        {
            tagBuilder.Attributes["disabled"] = "disabled";
        }

        foreach (var attr in Component.HtmlAttributes)
        {
            tagBuilder.Attributes[attr.Key] = attr.Value?.ToString() ?? string.Empty;
        }

        if (!string.IsNullOrEmpty(Component.AccessText))
        {
            var accessSpan = new TagBuilder("span");
            accessSpan.AddCssClass("hide-access");
            accessSpan.InnerHtml.AppendHtml(Component.AccessText);
            tagBuilder.InnerHtml.AppendHtml(accessSpan);
        }

        foreach (var item in Component.Items)
        {
            var option = new TagBuilder("option");
            option.Attributes["value"] = item.Value ?? string.Empty;
            if (item.Selected || item.Value == Component.SelectedValue)
            {
                option.Attributes["selected"] = "selected";
            }
            if (item.Disabled)
            {
                option.Attributes["disabled"] = "disabled";
            }
            option.InnerHtml.AppendHtml(item.Text ?? string.Empty);
            tagBuilder.InnerHtml.AppendHtml(option);
        }

        using (var writer = new StringWriter())
        {
            tagBuilder.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
