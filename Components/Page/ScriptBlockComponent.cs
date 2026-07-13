namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Page
{
    using System.Web.Mvc;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ScriptBlockComponent : ComponentBase
    {
        public ScriptBlockComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public override void WriteHtml(HtmlTextWriter writer) { }
        public override void WriteInitScript(HtmlTextWriter writer) { }
    }
}
