namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalTab
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class HorizontalTabBuilder : ComponentBuilderBase<HorizontalTabComponent, HorizontalTabBuilder>
    {
        public HorizontalTabBuilder(HorizontalTabComponent component) : base(component) { }
        public HorizontalTabBuilder(HorizontalTabComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
