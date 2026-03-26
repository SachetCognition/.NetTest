namespace Equant.SAV2000.ComponentLibrary.MVC.Components.BreadCrumbs
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class BreadCrumbsBuilder : ComponentBuilderBase<BreadCrumbsComponent, BreadCrumbsBuilder>
    {
        public BreadCrumbsBuilder(BreadCrumbsComponent component) : base(component) { }
        public BreadCrumbsBuilder(BreadCrumbsComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
