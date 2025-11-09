namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image
{
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageComponent : ComponentBase
    {
        public ImageComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            Src = string.Empty;
            Alt = string.Empty;
            CssClass = string.Empty;
            Width = string.Empty;
            Height = string.Empty;
        }

        public string Src { get; set; }
        public string Alt { get; set; }
        public string CssClass { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new System.Collections.Generic.List<JsResource>()); }
        }

        public string ToHtmlString()
        {
            using (var writer = new StringWriter())
            {
                WriteHtml(writer);
                return writer.ToString();
            }
        }

        public override void WriteHtml(TextWriter writer)
        {
            if (IsVisible)
            {
                var cssClassAttr = !string.IsNullOrEmpty(CssClass) ? $" class=\"{CssClass}\"" : "";
                var widthAttr = !string.IsNullOrEmpty(Width) ? $" width=\"{Width}\"" : "";
                var heightAttr = !string.IsNullOrEmpty(Height) ? $" height=\"{Height}\"" : "";
                writer.Write($"<img src=\"{System.Net.WebUtility.HtmlEncode(Src)}\" alt=\"{System.Net.WebUtility.HtmlEncode(Alt)}\" id=\"{Id}\"{cssClassAttr}{widthAttr}{heightAttr} />");
            }
        }

        public override void WriteInitScript(TextWriter writer)
        {
        }
    }
}
