namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Duration
{
    using System.Web.Mvc;
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DurationComponent : ComponentBase
    {
        public DurationComponent(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }

        public override void WriteHtml(HtmlTextWriter writer)
        {
            new DurationHtmlBuilder(this).Build(writer);
        }

        public override void WriteInitScript(HtmlTextWriter writer)
        {
        }
    }
}
