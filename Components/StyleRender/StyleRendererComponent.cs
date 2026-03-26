namespace Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender
{
    using System.Collections.ObjectModel;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.Web.Mvc;

    public class StyleRendererComponent : ComponentBase
    {
        public new static string ContextKey = "StyleRendererComponent";

        public StyleRendererComponent() { }
        public StyleRendererComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public StyleRendererComponent(HtmlHelper htmlHelper, ReadOnlyCollection<IStyleRendererComponent> renderers) : base(htmlHelper)
        {
        }

        public override void WriteHtml(System.Web.UI.HtmlTextWriter writer) { }
        public override void WriteInitScript(System.Web.UI.HtmlTextWriter writer) { }
    }
}
