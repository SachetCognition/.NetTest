namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DualList
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class DualListBuilder : ComponentBuilderBase<DualListComponent, DualListBuilder>
    {
        public DualListBuilder(DualListComponent component) : base(component) { }
        public DualListBuilder(DualListComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
