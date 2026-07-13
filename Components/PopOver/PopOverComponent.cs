namespace Equant.SAV2000.ComponentLibrary.MVC.Components.PopOver
{
    using System.Web.Mvc;
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class PopOverComponent : ComponentBase
    {
        public PopOverComponent(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }

        public override void WriteHtml(HtmlTextWriter writer)
        {
            new PopOverHtmlBuilder(this).Build(writer);
        }

        public override void WriteInitScript(HtmlTextWriter writer)
        {
        }
    }
}
