namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public class ImageToolTipComponent : ComponentBase
    {
        public ImageToolTipComponent() { }
        public ImageToolTipComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public string ToolTipId { get; set; }
        public string ImageAlt { get; set; }
        public string CssClass { get; set; }

        public override void WriteHtml(System.IO.TextWriter writer) { }
        public override void WriteInitScript(System.IO.TextWriter writer) { }
    }
}
