namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image
{
    using System.Web.Mvc;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageComponent : ComponentBase
    {
        public ImageComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public string Src { get; set; }
        public string Alt { get; set; }
        public string CssClass { get; set; }
        public ImageSrc ImageSource { get; set; }
        public override void WriteHtml(HtmlTextWriter writer) { }
        public override void WriteInitScript(HtmlTextWriter writer) { }
    }
}
