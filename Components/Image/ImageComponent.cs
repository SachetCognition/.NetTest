namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public class ImageComponent : ComponentBase
    {
        public ImageComponent() { }
        public ImageComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string Src { get; set; }
        public string Alt { get; set; }
        public string CssClass { get; set; }

        public override void WriteHtml(System.IO.TextWriter writer) { }
        public override void WriteInitScript(System.IO.TextWriter writer) { }
    }
}
