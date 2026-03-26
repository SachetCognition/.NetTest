namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ErrorComponent : ComponentBase
    {
        public ErrorComponent() { }
        public ErrorComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }

        public override void WriteHtml(System.Web.UI.HtmlTextWriter writer) { }
        public override void WriteInitScript(System.Web.UI.HtmlTextWriter writer) { }
    }

    public class ErrorComponents : ComponentBase
    {
        public ErrorComponents() { }
        public ErrorComponents(HtmlHelper htmlHelper) : base(htmlHelper) { }

        public override void WriteHtml(System.Web.UI.HtmlTextWriter writer) { }
        public override void WriteInitScript(System.Web.UI.HtmlTextWriter writer) { }
    }
}
