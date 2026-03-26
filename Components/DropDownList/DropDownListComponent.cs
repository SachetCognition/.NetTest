namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList
{
    using System.Web.Mvc;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DropDownListComponent : ComponentBase
    {
        public DropDownListComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public string CssClass { get; set; }
        public string OnChange { get; set; }
        public SelectList DataSource { get; set; }
        public bool IsDivNeeded { get; set; }
        public override void WriteHtml(HtmlTextWriter writer) { }
        public override void WriteInitScript(HtmlTextWriter writer) { }
    }
}
