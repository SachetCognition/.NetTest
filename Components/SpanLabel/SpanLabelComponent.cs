namespace Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel
{
    using System.Web.Mvc;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class SpanLabelComponent : ComponentBase
    {
        public SpanLabelComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public string Text { get; set; }
        public string CssClass { get; set; }
        public override void WriteHtml(HtmlTextWriter writer) { }
        public override void WriteInitScript(HtmlTextWriter writer) { }
    }
}
