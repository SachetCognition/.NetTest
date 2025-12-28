using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.Common.Resources;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;

/// <summary>
/// The custom label html builder.
/// </summary>
public class CustomLabelHtmlBuilder : HtmlBuilderBase<CustomLabelComponent>
{
    public CustomLabelHtmlBuilder(CustomLabelComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

#if DEBUG
        if (!Component.HtmlAttributes.ContainsKey("for"))
        {
            throw new ArgumentException("The attribute 'for' is mandatory to add with each label.");
        }
#endif
        var tagBuilderCustomLabel = new TagBuilder("label");
        
        foreach (var attr in Component.HtmlAttributes)
        {
            tagBuilderCustomLabel.Attributes[attr.Key] = attr.Value?.ToString() ?? string.Empty;
        }

        var sbCustomLabelInnerHtml = new StringBuilder();
        sbCustomLabelInnerHtml.Append(
            LabelHelper.InnerSpanTag(
                !string.IsNullOrEmpty(Component.Text) ? Component.Text : string.Empty,
                string.Empty,
                string.Empty,
                Component.IsHtmlEncode));

        if (!Component.IsOnlyForAccess)
        {
            if (string.IsNullOrEmpty(Component.CssClassLabel))
            {
                Component.CssClassLabel = "pull-right";
            }

            if (!string.IsNullOrEmpty(Component.CssClassLabel))
            {
                tagBuilderCustomLabel.AddCssClass(Component.CssClassLabel);
            }

            if (!string.IsNullOrEmpty(Component.SuperscriptText))
            {
                var superScriptCss = !string.IsNullOrEmpty(Component.SuperscriptCssClass) ? Component.SuperscriptCssClass : "importantfield";
                sbCustomLabelInnerHtml.Append(LabelHelper.InnerSpanTag(Component.SuperscriptText, superScriptCss, Component.SuperscriptToolTip, false));
            }

            if (Component.DisplayStar)
            {
                sbCustomLabelInnerHtml.Append(LabelHelper.InnerAbbrTag(ApplicationStrings.lblAsteriks,
                    string.Format(CultureInfo.CurrentCulture, ApplicationStrings.TIP000010, Component.Text)));
            }

            if (Component.DisplayColon)
            {
                sbCustomLabelInnerHtml.Append(LabelHelper.InnerSpanTag(ApplicationStrings.lblsemiColon, "paddingColon", string.Empty, false));
            }
            
            if (!string.IsNullOrEmpty(Component.AccessText))
            {
                sbCustomLabelInnerHtml.Append(LabelHelper.InnerSpanTag(Component.AccessText, "hide-access", string.Empty, false));
            }
        }
        else
        {
            tagBuilderCustomLabel.AddCssClass("hide-access");
        }

        tagBuilderCustomLabel.InnerHtml.AppendHtml(sbCustomLabelInnerHtml.ToString());

        using (var writer = new StringWriter())
        {
            tagBuilderCustomLabel.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
