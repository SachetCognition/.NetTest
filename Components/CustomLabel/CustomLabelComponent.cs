namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.Web.Mvc;

    public class CustomLabelComponent : ComponentBase
    {
        public CustomLabelComponent() { }
        public CustomLabelComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }

        public override string Id { get; set; }
        public string Text { get; set; }
        public string AccessText { get; set; }
        public string CssClass { get; set; }
        public string ForId { get; set; }
        public string AssociatedControlId { get; set; }
        public bool IsMandatory { get; set; }

        public override void WriteHtml(System.Web.UI.HtmlTextWriter writer) { }
        public override void WriteInitScript(System.Web.UI.HtmlTextWriter writer) { }
    }
}
