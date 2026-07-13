namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Page
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ScriptFileBuilder : ComponentBuilderBase<ScriptFileComponent, ScriptFileBuilder>
    {
        public ScriptFileBuilder(ScriptFileComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }
    }
}
