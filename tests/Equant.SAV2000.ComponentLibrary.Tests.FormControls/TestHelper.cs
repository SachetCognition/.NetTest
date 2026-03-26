namespace Equant.SAV2000.ComponentLibrary.Tests.FormControls
{
    using System.IO;
    using System.Text.Encodings.Web;
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using Microsoft.AspNetCore.Html;

    public static class TestHelper
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
            return ParseHtml(GetHtmlString(content));
        }
    }
}
