namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink
{
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HyperLinkComponent : ComponentBase
    {
        public HyperLinkComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            Text = string.Empty;
            Url = string.Empty;
        }

        public string Text { get; set; }
        public string Url { get; set; }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new System.Collections.Generic.List<JsResource>()); }
        }

        public override void WriteHtml(TextWriter writer)
        {
            if (IsVisible)
            {
                writer.Write($"<a href=\"{System.Net.WebUtility.HtmlEncode(Url)}\" id=\"{Id}\">{System.Net.WebUtility.HtmlEncode(Text)}</a>");
            }
        }

        public override void WriteInitScript(TextWriter writer)
        {
        }
    }
}
