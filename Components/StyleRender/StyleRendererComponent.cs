namespace Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender
{
    using System.Collections.ObjectModel;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public class StyleRendererComponent : ComponentBase
    {
        public new static string ContextKey = "StyleRendererComponent";

        public StyleRendererComponent() { }
        public StyleRendererComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }
        public StyleRendererComponent(IHtmlHelper htmlHelper, ReadOnlyCollection<IStyleRendererComponent> renderers) : base(htmlHelper)
        {
        }

        public override void WriteHtml(System.IO.TextWriter writer) { }
        public override void WriteInitScript(System.IO.TextWriter writer) { }
    }
}
