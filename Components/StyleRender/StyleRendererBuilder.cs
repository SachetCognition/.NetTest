namespace Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class StyleRendererBuilder : ComponentBuilderBase<StyleRendererComponent, StyleRendererBuilder>
    {
        public StyleRendererBuilder(StyleRendererComponent component) : base(component) { }
        public StyleRendererBuilder(StyleRendererComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }
    }
}
