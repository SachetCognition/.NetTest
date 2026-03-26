namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer
{
    using System.Collections.ObjectModel;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ScriptRendererComponent : ComponentBase
    {
        public static string ContextKey = "ScriptRendererComponent";

        public ScriptRendererComponent() { }
        public ScriptRendererComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }
        public ScriptRendererComponent(IHtmlHelper htmlHelper, ReadOnlyCollection<IScriptRendererComponent> renderers, bool isLightRequirement) : base(htmlHelper)
        {
        }

        public override IHtmlContent BuildHtml() { return HtmlString.Empty; }
        public override IHtmlContent BuildInitScript() { return HtmlString.Empty; }
    }
}
