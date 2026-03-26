namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ProgressBar
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ProgressBarBuilder : ComponentBuilderBase<ProgressBarComponent, ProgressBarBuilder>
    {
        public ProgressBarBuilder(ProgressBarComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public ProgressBarBuilder(ProgressBarComponent component)
            : base(component)
        {
        }

        public ProgressBarBuilder Value(int value) { this.Component.Value = value; return this; }
        public ProgressBarBuilder Max(int value) { this.Component.Max = value; return this; }
        public ProgressBarBuilder CssClass(string value) { this.Component.CssClass = value; return this; }
    }
}
