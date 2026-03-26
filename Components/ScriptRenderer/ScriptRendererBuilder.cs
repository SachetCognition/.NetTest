namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ScriptRendererBuilder : ComponentBuilderBase<ScriptRendererComponent, ScriptRendererBuilder>
    {
        public ScriptRendererBuilder(ScriptRendererComponent component) : base(component) { }
        public ScriptRendererBuilder(ScriptRendererComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }
    }
}
