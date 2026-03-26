namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink
{
    using System.Web.Mvc;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HyperLinkComponent : ComponentBase
    {
        public HyperLinkComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public string Text { get; set; }
        public string Url { get; set; }
        public string ActionUrl { get; set; }
        public string CssClass { get; set; }
        public string OnClick { get; set; }
        public string Target { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public override void WriteHtml(HtmlTextWriter writer) { }
        public override void WriteInitScript(HtmlTextWriter writer) { }
    }
}
