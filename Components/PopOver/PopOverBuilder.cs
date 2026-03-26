namespace Equant.SAV2000.ComponentLibrary.MVC.Components.PopOver
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class PopOverBuilder : ComponentBuilderBase<PopOverComponent, PopOverBuilder>
    {
        public PopOverBuilder(PopOverComponent component) : base(component) { }
        public PopOverBuilder(PopOverComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
