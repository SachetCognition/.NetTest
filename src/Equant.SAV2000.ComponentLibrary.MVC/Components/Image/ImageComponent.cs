namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image
{
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageComponent : ComponentBase
    {
        public ImageComponent() : base() { }
        public ImageComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string Src { get; set; }
        public string Alt { get; set; }
        public string Title { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }

        public override void WriteHtml(TextWriter writer)
        {
            new ImageHtmlBuilder(this).Build(writer);
        }

        public override void WriteInitScript(TextWriter writer) { }
    }
}
