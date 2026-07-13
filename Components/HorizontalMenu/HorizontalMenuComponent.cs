namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalMenu
{
    using System.Web.Mvc;
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HorizontalMenuComponent : ComponentBase
    {
        public HorizontalMenuComponent(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }

        public override void WriteHtml(HtmlTextWriter writer)
        {
            new HorizontalMenuHtmlBuilder(this).Build(writer);
        }

        public override void WriteInitScript(HtmlTextWriter writer)
        {
        }
    }
}
