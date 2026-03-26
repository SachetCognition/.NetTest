namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomHeader
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class CustomHeaderBuilder : ComponentBuilderBase<CustomHeaderComponent, CustomHeaderBuilder>
    {
        public CustomHeaderBuilder(CustomHeaderComponent component) : base(component) { }
        public CustomHeaderBuilder(CustomHeaderComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
