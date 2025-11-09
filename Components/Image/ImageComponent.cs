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
        }

        public string Src { get; set; }
        public string Alt { get; set; }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new System.Collections.Generic.List<JsResource>()); }
        }

        public override void WriteHtml(TextWriter writer)
        {
            if (IsVisible)
            {
                writer.Write($"<img src=\"{System.Net.WebUtility.HtmlEncode(Src)}\" alt=\"{System.Net.WebUtility.HtmlEncode(Alt)}\" id=\"{Id}\" />");
            }
        }

        public override void WriteInitScript(TextWriter writer)
        {
        }
    }
}
