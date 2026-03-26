namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.Web.Mvc;

    public class DropDownListComponent : ComponentBase
    {
        public DropDownListComponent() { }
        public DropDownListComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }

        public bool IsDivNeeded { get; set; }
        public CustomLabel.CustomLabelComponent Label { get; set; }

        public override void WriteHtml(System.Web.UI.HtmlTextWriter writer) { }
        public override void WriteInitScript(System.Web.UI.HtmlTextWriter writer) { }
    }
}
