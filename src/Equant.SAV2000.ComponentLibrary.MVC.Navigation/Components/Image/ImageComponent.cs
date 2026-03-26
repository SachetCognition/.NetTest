namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image
{
    using System.IO;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ImageComponent : ComponentBase
    {
        public ImageComponent() { }
        public ImageComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string Src { get; set; }
        public string Alt { get; set; }
        public string CssClass { get; set; }

        public override string ToHtmlString()
        {
            if (string.IsNullOrEmpty(Src))
                return string.Empty;

            var tag = new TagBuilder("img");
            tag.TagRenderMode = TagRenderMode.SelfClosing;
            if (!string.IsNullOrEmpty(Id))
                tag.Attributes["id"] = Id;
            tag.Attributes["src"] = Src;
            if (!string.IsNullOrEmpty(Alt))
                tag.Attributes["alt"] = Alt;
            if (!string.IsNullOrEmpty(CssClass))
                tag.AddCssClass(CssClass);

            using (var sw = new StringWriter())
            {
                tag.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                return sw.ToString();
            }
        }

        public override void WriteHtml(TextWriter writer) { }
        public override void WriteInitScript(TextWriter writer) { }
    }
}
