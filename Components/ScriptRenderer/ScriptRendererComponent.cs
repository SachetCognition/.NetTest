namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer
{
    using System.Collections.ObjectModel;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.Web.Mvc;

    public class ScriptRendererComponent : ComponentBase
    {
        public static string ContextKey = "ScriptRendererComponent";

        public ScriptRendererComponent() { }
        public ScriptRendererComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public ScriptRendererComponent(HtmlHelper htmlHelper, ReadOnlyCollection<IScriptRendererComponent> renderers, bool isLightRequirement) : base(htmlHelper)
        {
        }

        public override void WriteHtml(System.Web.UI.HtmlTextWriter writer) { }
        public override void WriteInitScript(System.Web.UI.HtmlTextWriter writer) { }
    }
}
