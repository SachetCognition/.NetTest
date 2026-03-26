namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Duration
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class DurationBuilder : ComponentBuilderBase<DurationComponent, DurationBuilder>
    {
        public DurationBuilder(DurationComponent component) : base(component) { }
        public DurationBuilder(DurationComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
