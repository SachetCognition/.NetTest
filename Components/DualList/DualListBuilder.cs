namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DualList
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DualListBuilder : ComponentBuilderBase<DualListComponent, DualListBuilder>
    {
        public DualListBuilder(DualListComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }
    }
}
