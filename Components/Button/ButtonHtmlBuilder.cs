using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Button;

/// <summary>
/// The Html Builder class for the Button component.
/// </summary>
public class ButtonHtmlBuilder : HtmlBuilderBase<ButtonComponent>
{
    public ButtonHtmlBuilder(ButtonComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

        var tagBuilder = new TagBuilder("input");
        tagBuilder.Attributes["id"] = Component.Id;

        if (!string.IsNullOrEmpty(Component.Name))
        {
            tagBuilder.Attributes["name"] = Component.Name;
        }

        tagBuilder.Attributes["type"] = "submit";
        
        if (!string.IsNullOrWhiteSpace(Component.DialogDivId))
        {
            tagBuilder.Attributes["data-toggle"] = "modal";
            tagBuilder.Attributes["data-target"] = "#" + Component.DialogDivId;
        }

        foreach (var attr in Component.HtmlAttributes)
        {
            tagBuilder.Attributes[attr.Key] = attr.Value?.ToString() ?? string.Empty;
        }

        var cssClass = Component.IsDisabled ? Component.CssClassReadOnly : Component.CssClass;
        if (!string.IsNullOrEmpty(cssClass))
        {
            tagBuilder.AddCssClass(cssClass);
        }

        tagBuilder.TagRenderMode = TagRenderMode.SelfClosing;
        return tagBuilder;
    }
}
