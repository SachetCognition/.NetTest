namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer
{
    using System.Web.Mvc;
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class BlockScriptRendererComponent : ComponentBase, IScriptRendererComponent
    {
        public BlockScriptRendererComponent(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }

        public override void WriteHtml(HtmlTextWriter writer)
        {
        }

        public override void WriteInitScript(HtmlTextWriter writer)
        {
        }
    }
}
