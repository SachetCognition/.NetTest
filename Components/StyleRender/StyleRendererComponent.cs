namespace Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender
{
    using System.Collections.ObjectModel;
    using System.Web.Mvc;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class StyleRendererComponent : ComponentBase
    {
        public static readonly string ContextKey = "StyleRendererComponent";

        public StyleRendererComponent(HtmlHelper htmlHelper, ReadOnlyCollection<IStyleRendererComponent> renderers)
            : base(htmlHelper)
        {
        }

        public override void WriteHtml(HtmlTextWriter writer) { }
        public override void WriteInitScript(HtmlTextWriter writer) { }
    }
}
