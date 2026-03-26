namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextBox
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.Web.Mvc;

    public class TextBoxComponent : ComponentBase
    {

        public TextBoxComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public string Value { get; set; }
        public string CssClass { get; set; }
        public string Placeholder { get; set; }
        public int MaxLength { get; set; }

        public override void WriteHtml(System.Web.UI.HtmlTextWriter writer) { }
        public override void WriteInitScript(System.Web.UI.HtmlTextWriter writer) { }
    }
}
