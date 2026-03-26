namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip
{
    using System;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageToolTipHtmlBuilder : HtmlBuilderBase<ImageToolTipComponent>
    {
        public ImageToolTipHtmlBuilder(ImageToolTipComponent component) : base(component) { }

        public override IHtmlContent Build()
        {
            var outerSpan = new TagBuilder("span");

            if (!string.IsNullOrEmpty(Component.Id))
            {
                outerSpan.MergeAttribute("id", Component.Id);
            }

            if (!string.IsNullOrEmpty(Component.CssClassSpan))
            {
                outerSpan.AddCssClass(Component.CssClassSpan);
            }

            foreach (var attr in Component.HtmlAttributes)
            {
                outerSpan.MergeAttribute(attr.Key, attr.Value?.ToString());
            }

            // Create the image element
            var imgTag = new TagBuilder("img");
            imgTag.TagRenderMode = TagRenderMode.SelfClosing;

            if (!string.IsNullOrEmpty(Component.ImageUrl))
            {
                imgTag.MergeAttribute("src", Component.ImageUrl);
            }

            if (!string.IsNullOrEmpty(Component.ImageAlt))
            {
                imgTag.MergeAttribute("alt", Component.ImageAlt);
            }
            else
            {
                imgTag.MergeAttribute("alt", string.Empty);
            }

            if (!string.IsNullOrEmpty(Component.CssClass))
            {
                imgTag.AddCssClass(Component.CssClass);
            }

            if (!string.IsNullOrEmpty(Component.Title))
            {
                imgTag.MergeAttribute("title", Component.Title);
            }

            outerSpan.InnerHtml.AppendHtml(imgTag);

            // Add tooltip content span
            if (!string.IsNullOrEmpty(Component.Text))
            {
                var tooltipSpan = new TagBuilder("span");
                if (!string.IsNullOrEmpty(Component.ToolTipId))
                {
                    tooltipSpan.MergeAttribute("id", Component.ToolTipId);
                }
                if (!string.IsNullOrEmpty(Component.CssClassInnerSpan))
                {
                    tooltipSpan.AddCssClass(Component.CssClassInnerSpan);
                }
                tooltipSpan.InnerHtml.AppendHtml(Component.Text);
                outerSpan.InnerHtml.AppendHtml(tooltipSpan);
            }

            // Add data attribute for persistence mode
            outerSpan.MergeAttribute("data-persistence-mode", Component.PersistanceMode == PersistanceMode.Click ? "click" : "hover");

            return outerSpan;
        }
    }
}
