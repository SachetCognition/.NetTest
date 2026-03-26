namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip
{
    using System.Web.Mvc;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public enum PersistanceMode
    {
        Click,
        Hover
    }

    public class ImageToolTipComponent : ComponentBase
    {
        public ImageToolTipComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public string ToolTip { get; set; }
        public string CssClass { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public string CssClassImage { get; set; }
        public string ToolTipId { get; set; }
        public PersistanceMode PersistanceMode { get; set; }
        public override void WriteHtml(HtmlTextWriter writer) { }
        public override void WriteInitScript(HtmlTextWriter writer) { }
    }
}
