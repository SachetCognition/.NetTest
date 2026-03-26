namespace Equant.SAV2000.ComponentLibrary.Tests.DateTime.Helpers
{
    using System.IO;
    using System.Text.Encodings.Web;
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public static class HtmlTestHelper
    {
        public static string GetHtmlString(IHtmlContent content)
        {
            using (var writer = new StringWriter())
            {
                content.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }

        public static IDocument ParseHtml(string html)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var parser = context.GetService<IHtmlParser>();
            return parser.ParseDocument(html);
        }

        public static IDocument ParseHtml(IHtmlContent content)
        {
            var html = GetHtmlString(content);
            return ParseHtml(html);
        }

        public static string GetTagBuilderHtml(TagBuilder tagBuilder)
        {
            using (var writer = new StringWriter())
            {
                tagBuilder.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}
