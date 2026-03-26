namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ProgressBar
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class ProgressBarBuilder : ComponentBuilderBase<ProgressBarComponent, ProgressBarBuilder>
    {
        public ProgressBarBuilder(ProgressBarComponent component) : base(component) { }
        public ProgressBarBuilder(ProgressBarComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
