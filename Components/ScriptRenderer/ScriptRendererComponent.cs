namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer
{
    using System.Collections.ObjectModel;
    using System.Web.Mvc;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ScriptRendererComponent : ComponentBase
    {
        public static readonly string ContextKey = "ScriptRendererComponent";

        public ScriptRendererComponent(HtmlHelper htmlHelper, ReadOnlyCollection<IScriptRendererComponent> renderers, bool isLightRequirement)
            : base(htmlHelper)
        {
        }

        public override void WriteHtml(HtmlTextWriter writer) { }
        public override void WriteInitScript(HtmlTextWriter writer) { }
    }
}
