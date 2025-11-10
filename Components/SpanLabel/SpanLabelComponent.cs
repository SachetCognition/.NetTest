namespace Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel
{
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class SpanLabelComponent : ComponentBase
    {
        public SpanLabelComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            Text = string.Empty;
        }

        public string Text { get; set; }
        public string CssClass { get; set; }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new System.Collections.Generic.List<JsResource>()); }
        }

        public override void WriteHtml(TextWriter writer)
        {
            if (IsVisible)
            {
                writer.Write($"<span class=\"{CssClass}\" id=\"{Id}\">{System.Net.WebUtility.HtmlEncode(Text)}</span>");
            }
        }

        public override void WriteInitScript(TextWriter writer)
        {
        }

        public string ToHtmlString()
        {
            using (var writer = new StringWriter())
            {
                WriteHtml(writer);
                return writer.ToString();
            }
        }
    }
}
