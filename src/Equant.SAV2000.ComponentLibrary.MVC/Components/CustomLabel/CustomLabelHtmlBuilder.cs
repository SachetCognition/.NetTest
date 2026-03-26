namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel
{
    using System.IO;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CustomLabelHtmlBuilder : HtmlBuilderBase<CustomLabelComponent>
    {
        public CustomLabelHtmlBuilder(CustomLabelComponent component) : base(component) { }

        public override IHtmlContent Build()
        {
            var tagBuilder = new TagBuilder("label");

            if (!string.IsNullOrEmpty(Component.Id))
            {
                tagBuilder.MergeAttribute("id", Component.Id);
            }

            if (!string.IsNullOrEmpty(Component.CssClassLabel))
            {
                tagBuilder.AddCssClass(Component.CssClassLabel);
            }

            // Apply for attribute from HtmlAttributes
            foreach (var attr in Component.HtmlAttributes)
            {
                tagBuilder.MergeAttribute(attr.Key, attr.Value?.ToString());
            }

            if (Component.IsOnlyForAccess)
            {
                tagBuilder.AddCssClass("hide-access");
            }

            var innerHtml = new HtmlContentBuilder();

            // Add mandatory star indicator
            if (Component.IsMandatory || Component.DisplayStar)
            {
                var abbrTag = new TagBuilder("abbr");
                abbrTag.AddCssClass("required");
                abbrTag.InnerHtml.Append("*");
                innerHtml.AppendHtml(abbrTag);
            }

            // Add label text
            if (!string.IsNullOrEmpty(Component.Text))
            {
                if (Component.IsHtmlEncode)
                {
                    innerHtml.Append(Component.Text);
                }
                else
                {
                    innerHtml.AppendHtml(Component.Text);
                }
            }

            // Add colon
            if (Component.DisplayColon)
            {
                innerHtml.AppendHtml(" :");
            }

            // Add superscript
            if (!string.IsNullOrEmpty(Component.SuperscriptText))
            {
                var supTag = new TagBuilder("sup");
                if (!string.IsNullOrEmpty(Component.SuperscriptCssClass))
                {
                    supTag.AddCssClass(Component.SuperscriptCssClass);
                }
                if (!string.IsNullOrEmpty(Component.SuperscriptToolTip))
                {
                    supTag.MergeAttribute("title", Component.SuperscriptToolTip);
                }
                supTag.InnerHtml.Append(Component.SuperscriptText);
                innerHtml.AppendHtml(supTag);
            }

            // Add accessibility text
            if (!string.IsNullOrEmpty(Component.AccessText))
            {
                var accessSpan = new TagBuilder("span");
                accessSpan.AddCssClass("hide-access");
                accessSpan.InnerHtml.Append(Component.AccessText);
                innerHtml.AppendHtml(accessSpan);
            }

            tagBuilder.InnerHtml.AppendHtml(innerHtml);

            return tagBuilder;
        }
    }
}
