using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ActionButton;

/// <summary>
/// The Html Builder class for the ActionButton component.
/// </summary>
public class ActionButtonHtmlBuilder : HtmlBuilderBase<ActionButtonComponent>
{
    public ActionButtonHtmlBuilder(ActionButtonComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

        var tbActionButton = new TagBuilder("button");
        tbActionButton.Attributes["id"] = Component.Id;

        if (!string.IsNullOrEmpty(Component.Name))
        {
            tbActionButton.Attributes["name"] = Component.Name;
        }

        if (!string.IsNullOrWhiteSpace(Component.DialogBoxId))
        {
            tbActionButton.Attributes["data-toggle"] = "modal";
            tbActionButton.Attributes["data-target"] = "#" + Component.DialogBoxId;
        }

        tbActionButton.Attributes["type"] = "submit";
        
        foreach (var attr in Component.HtmlAttributes)
        {
            tbActionButton.Attributes[attr.Key] = attr.Value?.ToString() ?? string.Empty;
        }

        var cssClass = Component.IsDisabled ? Component.CssClassReadOnly : Component.CssClass;
        if (!string.IsNullOrEmpty(cssClass))
        {
            tbActionButton.AddCssClass(cssClass);
        }

        if (!string.IsNullOrEmpty(Component.AccessText))
        {
            var tbAccSpan = new TagBuilder("span");
            tbAccSpan.AddCssClass("hide-access");
            tbAccSpan.InnerHtml.AppendHtml(Component.AccessText);
            tbActionButton.InnerHtml.AppendHtml(tbAccSpan);
        }

        var tbTextSpan = new TagBuilder("span");
        if (!string.IsNullOrEmpty(Component.CssSpan))
        {
            tbTextSpan.AddCssClass(Component.CssSpan);
        }

        tbTextSpan.InnerHtml.AppendHtml(Component.Text ?? string.Empty);
        tbActionButton.InnerHtml.AppendHtml(tbTextSpan);

        using (var writer = new StringWriter())
        {
            tbActionButton.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
