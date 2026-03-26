namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HyperLinkHtmlBuilder : HtmlBuilderBase<HyperLinkComponent>
    {
        public HyperLinkHtmlBuilder(HyperLinkComponent component) : base(component) { }

        public override IHtmlContent Build()
        {
            var tagBuilder = new TagBuilder("a");

            if (!string.IsNullOrEmpty(Component.Id))
            {
                tagBuilder.MergeAttribute("id", Component.Id);
            }

            if (!string.IsNullOrEmpty(Component.Name))
            {
                tagBuilder.MergeAttribute("name", Component.Name);
            }

            var href = Component.Href ?? Component.ActionUrl ?? "#";
            tagBuilder.MergeAttribute("href", href);

            if (!string.IsNullOrEmpty(Component.CssClass))
            {
                tagBuilder.AddCssClass(Component.CssClass);
            }

            if (!string.IsNullOrEmpty(Component.Target))
            {
                tagBuilder.MergeAttribute("target", Component.Target);
            }

            if (!string.IsNullOrEmpty(Component.Title))
            {
                tagBuilder.MergeAttribute("title", Component.Title);
            }

            foreach (var attr in Component.HtmlAttributes)
            {
                tagBuilder.MergeAttribute(attr.Key, attr.Value?.ToString());
            }

            // If ImageUrl is set, render an image inside the anchor
            if (!string.IsNullOrEmpty(Component.ImageUrl))
            {
                var imgTag = new TagBuilder("img");
                imgTag.TagRenderMode = TagRenderMode.SelfClosing;
                imgTag.MergeAttribute("src", Component.ImageUrl);
                imgTag.MergeAttribute("alt", Component.Text ?? string.Empty);
                tagBuilder.InnerHtml.AppendHtml(imgTag);
            }

            if (!string.IsNullOrEmpty(Component.Text))
            {
                tagBuilder.InnerHtml.Append(Component.Text);
            }

            return tagBuilder;
        }
    }
}
