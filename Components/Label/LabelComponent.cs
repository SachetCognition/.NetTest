namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.Web.Mvc;

    public class LabelComponent : ComponentBase
    {

        public LabelComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public string Text { get; set; }
        public string CssClass { get; set; }

        public override void WriteHtml(System.Web.UI.HtmlTextWriter writer) { }
        public override void WriteInitScript(System.Web.UI.HtmlTextWriter writer) { }
    }
}
