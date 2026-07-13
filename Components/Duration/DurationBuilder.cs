namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Duration
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DurationBuilder : ComponentBuilderBase<DurationComponent, DurationBuilder>
    {
        public DurationBuilder(DurationComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }
    }
}
