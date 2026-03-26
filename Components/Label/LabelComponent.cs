namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label
{
    using System.Web.Mvc;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class LabelComponent : ComponentBase
    {
        public LabelComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public string Text { get; set; }
        public string CssClass { get; set; }
        public string CssClassLabel { get; set; }
        public string For { get; set; }
        public override void WriteHtml(HtmlTextWriter writer) { }
        public override void WriteInitScript(HtmlTextWriter writer) { }
    }
}
