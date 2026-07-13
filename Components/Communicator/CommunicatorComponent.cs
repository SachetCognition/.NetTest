namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Communicator
{
    using System.Web.Mvc;
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CommunicatorComponent : ComponentBase
    {
        public CommunicatorComponent(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }

        public override void WriteHtml(HtmlTextWriter writer)
        {
            new CommunicatorHtmlBuilder(this).Build(writer);
        }

        public override void WriteInitScript(HtmlTextWriter writer)
        {
        }
    }
}
