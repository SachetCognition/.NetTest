using System.Globalization;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ClickToVoice;

public class ClickToVoiceHtmlBuilder : HtmlBuilderBase<ClickToVoiceComponent>
{
    private const string ClickToVoiceImageUrl = "/Images/picto-tel.png";

    public ClickToVoiceHtmlBuilder(ClickToVoiceComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

        var tagBuilderAnchor = new TagBuilder("a");
        tagBuilderAnchor.Attributes["id"] = Component.Id;

        if (string.IsNullOrEmpty(Component.Title))
        {
            Component.Title = string.Format(CultureInfo.CurrentCulture, "Click to call {0}", Component.TelephoneNumber);
        }

        tagBuilderAnchor.Attributes["title"] = Component.Title;
        tagBuilderAnchor.Attributes["href"] = "###";

        foreach (var attr in Component.HtmlAttributes)
        {
            tagBuilderAnchor.Attributes[attr.Key] = attr.Value?.ToString() ?? string.Empty;
        }

        if (!string.IsNullOrEmpty(Component.CssAnchorTag))
        {
            tagBuilderAnchor.AddCssClass(Component.CssAnchorTag);
        }

        if (string.IsNullOrEmpty(Component.ImageUrl))
        {
            Component.ImageUrl = ClickToVoiceImageUrl;
        }

        tagBuilderAnchor.InnerHtml.AppendHtml(CreateImageTag());

        if (!string.IsNullOrEmpty(Component.TelephoneNumber) && Component.IsNumberSpanVisible)
        {
            var tbtelnoSpan = new TagBuilder("span");
            if (!string.IsNullOrEmpty(Component.CssTelephoneNumberSpan))
            {
                tbtelnoSpan.AddCssClass(Component.CssTelephoneNumberSpan);
            }
            tbtelnoSpan.InnerHtml.AppendHtml(Component.TelephoneNumber);
            tagBuilderAnchor.InnerHtml.AppendHtml(tbtelnoSpan);
        }

        using var writer = new StringWriter();
        tagBuilderAnchor.WriteTo(writer, HtmlEncoder.Default);
        return new HtmlString(writer.ToString());
    }

    private TagBuilder CreateImageTag()
    {
        var tagBuilderImage = new TagBuilder("img");

        if (string.IsNullOrEmpty(Component.AlternateText))
        {
            Component.AlternateText = Component.Title;
        }

        if (!string.IsNullOrEmpty(Component.ImageUrl))
        {
            tagBuilderImage.Attributes["src"] = Component.ImageUrl;
        }

        tagBuilderImage.Attributes["alt"] = Component.AlternateText ?? string.Empty;

        if (!string.IsNullOrEmpty(Component.CssClassImage))
        {
            tagBuilderImage.AddCssClass(Component.CssClassImage);
        }

        tagBuilderImage.TagRenderMode = TagRenderMode.SelfClosing;
        return tagBuilderImage;
    }
}
