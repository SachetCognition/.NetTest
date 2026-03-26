namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer
{
    using System.Collections.ObjectModel;
    using System.Web.Mvc;
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ScriptRendererComponent : ComponentBase
    {
        public static readonly string ContextKey = "ScriptRendererComponent";

        private readonly ReadOnlyCollection<IScriptRendererComponent> scriptRenderers;
        private readonly bool isLightRequirement;

        public ScriptRendererComponent(HtmlHelper htmlHelper, ReadOnlyCollection<IScriptRendererComponent> scriptRenderers, bool isLightRequirement)
            : base(htmlHelper)
        {
            this.scriptRenderers = scriptRenderers;
            this.isLightRequirement = isLightRequirement;
        }

        public ReadOnlyCollection<IScriptRendererComponent> ScriptRenderers
        {
            get { return this.scriptRenderers; }
        }

        public bool IsLightRequirement
        {
            get { return this.isLightRequirement; }
        }

        public override void WriteHtml(HtmlTextWriter writer)
        {
        }

        public override void WriteInitScript(HtmlTextWriter writer)
        {
        }
    }
}
