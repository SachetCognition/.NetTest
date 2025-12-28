using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBox;

/// <summary>
/// The check box html builder.
/// </summary>
public class CheckBoxHtmlBuilder : HtmlBuilderBase<CheckBoxComponent>
{
    public CheckBoxHtmlBuilder(CheckBoxComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

        var sb = new StringBuilder();

        var tagBuilderHidden = new TagBuilder("input");
        tagBuilderHidden.Attributes["id"] = string.Format(CultureInfo.InvariantCulture, "{0}Hidden", Component.Id);

        if (!string.IsNullOrEmpty(Component.Name))
        {
            tagBuilderHidden.Attributes["name"] = Component.Name;
        }

        tagBuilderHidden.Attributes["type"] = "hidden";
        tagBuilderHidden.Attributes["value"] = Component.IsChecked ? "true" : "false";
        tagBuilderHidden.TagRenderMode = TagRenderMode.SelfClosing;

        var tagBuilderCheckBox = new TagBuilder("input");
        tagBuilderCheckBox.Attributes["id"] = Component.Id;
        
        if (!string.IsNullOrEmpty(Component.Name))
        {
            tagBuilderCheckBox.Attributes["name"] = Component.Name;
        }
        
        tagBuilderCheckBox.Attributes["type"] = "checkbox";
        tagBuilderCheckBox.Attributes["value"] = Component.IsChecked ? "true" : "false";

        foreach (var attr in Component.HtmlAttributes)
        {
            tagBuilderCheckBox.Attributes[attr.Key] = attr.Value?.ToString() ?? string.Empty;
        }

        if (Component.IsDisabled)
        {
            Component.CssClass = Component.CssClassDisabled;
            tagBuilderCheckBox.Attributes["disabled"] = "disabled";
        }

        if (!string.IsNullOrEmpty(Component.CssClass))
        {
            tagBuilderCheckBox.AddCssClass(Component.CssClass);
        }

        if (!string.IsNullOrEmpty(Component.Title))
        {
            tagBuilderCheckBox.Attributes["title"] = Component.Title;
        }

        tagBuilderCheckBox.TagRenderMode = TagRenderMode.SelfClosing;

        using (var writer = new StringWriter())
        {
            tagBuilderHidden.WriteTo(writer, HtmlEncoder.Default);
            tagBuilderCheckBox.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
