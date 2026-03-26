namespace Equant.SAV2000.ComponentLibrary.MVC.Extensions
{
    using System.IO;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// Extension methods for TagBuilder to provide ASP.NET Core equivalents
    /// of legacy TagBuilder.ToString(TagRenderMode) calls.
    /// </summary>
    public static class TagBuilderExtensions
    {
        /// <summary>
        /// Renders the TagBuilder to an HTML string using the specified render mode.
        /// Replaces the legacy TagBuilder.ToString(TagRenderMode) from System.Web.Mvc.
        /// </summary>
        public static string ToHtmlString(this TagBuilder tagBuilder, TagRenderMode renderMode)
        {
            tagBuilder.TagRenderMode = renderMode;
            using (var writer = new StringWriter())
            {
                tagBuilder.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }

        /// <summary>
        /// Renders the TagBuilder to an HTML string using TagRenderMode.Normal.
        /// Replaces implicit TagBuilder.ToString() calls from System.Web.Mvc.
        /// </summary>
        public static string ToHtmlString(this TagBuilder tagBuilder)
        {
            return ToHtmlString(tagBuilder, TagRenderMode.Normal);
        }

        /// <summary>
        /// Renders the IHtmlContent to a string.
        /// </summary>
        public static string ToHtmlString(this IHtmlContent htmlContent)
        {
            using (var writer = new StringWriter())
            {
                htmlContent.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}
