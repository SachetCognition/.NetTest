namespace Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender
{
    using System.Collections.ObjectModel;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public class StyleRenderComponent : ComponentBase
    {
        public string ContextKey { get; set; }

        public StyleRenderComponent() { }
        public StyleRenderComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }
        public StyleRenderComponent(IHtmlHelper htmlHelper, string contextKey) : base(htmlHelper)
        {
            this.ContextKey = contextKey;
        }

        public override void WriteHtml(System.IO.TextWriter writer) { }
        public override void WriteInitScript(System.IO.TextWriter writer) { }
    }

}
