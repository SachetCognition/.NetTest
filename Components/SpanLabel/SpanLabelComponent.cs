namespace Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.Web.Mvc;

    public class SpanLabelComponent : ComponentBase
    {
        public SpanLabelComponent() { }
        public SpanLabelComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }

        public string Text { get; set; }
        public string CssClass { get; set; }

        public override void WriteHtml(System.Web.UI.HtmlTextWriter writer) { }
        public override void WriteInitScript(System.Web.UI.HtmlTextWriter writer) { }
    }
}
