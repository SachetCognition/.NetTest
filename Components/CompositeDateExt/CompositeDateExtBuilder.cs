namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDateExt
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class CompositeDateExtBuilder : ComponentBuilderBase<CompositeDateExtComponent, CompositeDateExtBuilder>
    {
        public CompositeDateExtBuilder(CompositeDateExtComponent component) : base(component) { }
        public CompositeDateExtBuilder(CompositeDateExtComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
