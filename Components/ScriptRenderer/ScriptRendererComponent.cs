namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer
{
    using System.Collections.ObjectModel;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public class ScriptRendererComponent : ComponentBase
    {
        public static string ContextKey = "ScriptRendererComponent";

        public ScriptRendererComponent() { }
        public ScriptRendererComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }
        public ScriptRendererComponent(IHtmlHelper htmlHelper, ReadOnlyCollection<IScriptRendererComponent> renderers, bool isLightRequirement) : base(htmlHelper)
        {
        }

        public override void WriteHtml(System.IO.TextWriter writer) { }
        public override void WriteInitScript(System.IO.TextWriter writer) { }
    }
}
