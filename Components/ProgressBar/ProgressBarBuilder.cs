namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ProgressBar
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ProgressBarBuilder : ComponentBuilderBase<ProgressBarComponent, ProgressBarBuilder>
    {
        public ProgressBarBuilder(ProgressBarComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }
    }
}
