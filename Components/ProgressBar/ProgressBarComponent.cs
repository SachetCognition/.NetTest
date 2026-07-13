namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ProgressBar
{
    using System.Web.Mvc;
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ProgressBarComponent : ComponentBase
    {
        public ProgressBarComponent(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }

        public override void WriteHtml(HtmlTextWriter writer)
        {
            new ProgressBarHtmlBuilder(this).Build(writer);
        }

        public override void WriteInitScript(HtmlTextWriter writer)
        {
        }
    }
}
