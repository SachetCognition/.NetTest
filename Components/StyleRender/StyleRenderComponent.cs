namespace Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender
{
    using System.Collections.ObjectModel;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.Web.Mvc;

    public class StyleRenderComponent : ComponentBase
    {
        public string ContextKey { get; set; }

        public StyleRenderComponent() { }
        public StyleRenderComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public StyleRenderComponent(HtmlHelper htmlHelper, string contextKey) : base(htmlHelper)
        {
            this.ContextKey = contextKey;
        }

        public override void WriteHtml(System.Web.UI.HtmlTextWriter writer) { }
        public override void WriteInitScript(System.Web.UI.HtmlTextWriter writer) { }
    }

}
