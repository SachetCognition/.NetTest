namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalMenu
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class HorizontalMenuBuilder : ComponentBuilderBase<HorizontalMenuComponent, HorizontalMenuBuilder>
    {
        public HorizontalMenuBuilder(HorizontalMenuComponent component) : base(component) { }
        public HorizontalMenuBuilder(HorizontalMenuComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
