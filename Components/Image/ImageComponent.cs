namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.Web.Mvc;

    public class ImageComponent : ComponentBase
    {
        public ImageComponent() { }
        public ImageComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }

        public string Src { get; set; }
        public string Alt { get; set; }
        public string CssClass { get; set; }

        public override void WriteHtml(System.Web.UI.HtmlTextWriter writer) { }
        public override void WriteInitScript(System.Web.UI.HtmlTextWriter writer) { }
    }
}
