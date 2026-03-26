namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Popup
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class PopupImageBuilder : ComponentBuilderBase<PopupImageComponent, PopupImageBuilder>
    {
        public PopupImageBuilder(PopupImageComponent component) : base(component) { }
        public PopupImageBuilder(PopupImageComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
