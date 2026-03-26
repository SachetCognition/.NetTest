namespace Equant.SAV2000.ComponentLibrary.Tests.LabelsTooltips
{
    using System.IO;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Html;

    public static class HtmlContentHelper
    {
        public static string ToHtmlString(this IHtmlContent content)
        {
            using var writer = new StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}
