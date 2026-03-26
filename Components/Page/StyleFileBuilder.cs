namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Page
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class StyleFileBuilder : ComponentBuilderBase<StyleFileComponent, StyleFileBuilder>
    {
        public StyleFileBuilder(StyleFileComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }
    }
}
