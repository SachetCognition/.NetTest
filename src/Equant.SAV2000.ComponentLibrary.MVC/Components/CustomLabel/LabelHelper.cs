namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel
{
    using System.IO;
    using System.Net;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public static class LabelHelper
    {
        public static IHtmlContent InnerSpanTag(string spanText, string spanCssClass, string spanToolTip, bool isHtmlEncode)
        {
            var tagBuilderSpan = new TagBuilder("span");

            if (!string.IsNullOrEmpty(spanToolTip))
            {
                tagBuilderSpan.MergeAttribute("title", spanToolTip);
            }

            if (!string.IsNullOrEmpty(spanCssClass))
            {
                tagBuilderSpan.AddCssClass(spanCssClass);
            }

            if (isHtmlEncode)
            {
                tagBuilderSpan.InnerHtml.Append(spanText);
            }
            else
            {
                tagBuilderSpan.InnerHtml.AppendHtml(spanText);
            }

            return tagBuilderSpan;
        }

        public static IHtmlContent InnerAbbrTag(string abbrText, string spanToolTip)
        {
            var tagBuilderSpan = new TagBuilder("abbr");

            if (!string.IsNullOrEmpty(spanToolTip))
            {
                tagBuilderSpan.MergeAttribute("title", spanToolTip);
            }

            tagBuilderSpan.AddCssClass("required");
            tagBuilderSpan.InnerHtml.AppendHtml(abbrText);

            return tagBuilderSpan;
        }
    }
}
