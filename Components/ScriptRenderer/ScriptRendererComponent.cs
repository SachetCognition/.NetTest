namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer
{
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ScriptRendererComponent : ComponentBase
    {
        public const string ContextKey = "ScriptRendererComponent";

        public ScriptRendererComponent(IHtmlHelper htmlHelper, ReadOnlyCollection<object> renderers, bool isLightRequirement)
            : base(htmlHelper)
        {
        }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new System.Collections.Generic.List<JsResource>()); }
        }

        public override void WriteHtml(TextWriter writer)
        {
        }

        public override void WriteInitScript(TextWriter writer)
        {
        }
    }

    public class ScriptRendererBuilder : ComponentBuilderBase<ScriptRendererComponent, ScriptRendererBuilder>
    {
        public ScriptRendererBuilder(ScriptRendererComponent component, Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
        }
    }

    public class ClientDependencyScriptRendererComponent
    {
        public ClientDependencyScriptRendererComponent(IHtmlHelper htmlHelper)
        {
        }
    }

    public class BlockScriptRendererComponent
    {
        public BlockScriptRendererComponent(IHtmlHelper htmlHelper)
        {
        }
    }
}
