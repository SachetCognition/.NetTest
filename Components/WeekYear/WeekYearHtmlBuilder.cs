using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;

public class WeekYearHtmlBuilder : HtmlBuilderBase<WeekYearComponent>
{
    public WeekYearHtmlBuilder(WeekYearComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

        var container = new TagBuilder("div");
        container.Attributes["id"] = Component.Id;

        if (!string.IsNullOrEmpty(Component.CssClass))
        {
            container.AddCssClass(Component.CssClass);
        }

        foreach (var attr in Component.HtmlAttributes)
        {
            container.Attributes[attr.Key] = attr.Value?.ToString() ?? string.Empty;
        }

        // Week input
        var weekInput = new TagBuilder("input");
        weekInput.Attributes["type"] = "text";
        weekInput.Attributes["id"] = $"{Component.Id}_Week";
        weekInput.Attributes["name"] = $"{Component.Name}.Week";
        weekInput.Attributes["value"] = Component.Value.Week.ToString("D2");
        if (Component.IsDisabled)
        {
            weekInput.Attributes["disabled"] = "disabled";
        }
        container.InnerHtml.AppendHtml(weekInput);

        // Separator
        container.InnerHtml.AppendHtml("/");

        // Year input
        var yearInput = new TagBuilder("input");
        yearInput.Attributes["type"] = "text";
        yearInput.Attributes["id"] = $"{Component.Id}_Year";
        yearInput.Attributes["name"] = $"{Component.Name}.Year";
        yearInput.Attributes["value"] = Component.Value.Year.ToString();
        if (Component.IsDisabled)
        {
            yearInput.Attributes["disabled"] = "disabled";
        }
        container.InnerHtml.AppendHtml(yearInput);

        using (var writer = new StringWriter())
        {
            container.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
