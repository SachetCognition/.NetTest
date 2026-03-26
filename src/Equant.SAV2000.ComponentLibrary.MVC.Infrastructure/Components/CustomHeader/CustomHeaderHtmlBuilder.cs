namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomHeader
{
    using System.IO;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    /// <summary>
    /// Renders a custom header section.
    /// </summary>
    public class CustomHeaderHtmlBuilder : HtmlBuilderBase<CustomHeaderComponent>
    {
        public CustomHeaderHtmlBuilder(CustomHeaderComponent component) : base(component) { }

        public override IHtmlContent Build()
        {
            var headerDiv = new TagBuilder("div");
            headerDiv.AddCssClass("sav-custom-header");

            if (!string.IsNullOrEmpty(Component.CssClass))
            {
                headerDiv.AddCssClass(Component.CssClass);
            }

            if (!string.IsNullOrEmpty(Component.Id))
            {
                headerDiv.Attributes["id"] = Component.Id;
            }

            // Icon
            if (!string.IsNullOrEmpty(Component.IconCssClass))
            {
                var iconSpan = new TagBuilder("span");
                iconSpan.AddCssClass(Component.IconCssClass);
                headerDiv.InnerHtml.AppendHtml(iconSpan);
            }

            // Title
            if (!string.IsNullOrEmpty(Component.Title))
            {
                var titleTag = new TagBuilder("h3");
                titleTag.AddCssClass("sav-header-title");
                titleTag.InnerHtml.Append(Component.Title);
                headerDiv.InnerHtml.AppendHtml(titleTag);
            }

            // Subtitle
            if (!string.IsNullOrEmpty(Component.SubTitle))
            {
                var subTitleTag = new TagBuilder("span");
                subTitleTag.AddCssClass("sav-header-subtitle");
                subTitleTag.InnerHtml.Append(Component.SubTitle);
                headerDiv.InnerHtml.AppendHtml(subTitleTag);
            }

            // Add HTML attributes
            foreach (var attr in Component.HtmlAttributes)
            {
                headerDiv.Attributes[attr.Key] = attr.Value?.ToString();
            }

            var writer = new StringWriter();
            headerDiv.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
