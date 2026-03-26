namespace Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender
{
    using System.Collections.ObjectModel;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class StyleRendererComponent : ComponentBase
    {
        public new static string ContextKey = "StyleRendererComponent";

        public StyleRendererComponent() { }
        public StyleRendererComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }
        public StyleRendererComponent(IHtmlHelper htmlHelper, ReadOnlyCollection<IStyleRendererComponent> renderers) : base(htmlHelper)
        {
        }

        public override IHtmlContent BuildHtml() { return HtmlString.Empty; }
        public override IHtmlContent BuildInitScript() { return HtmlString.Empty; }
    }
}
