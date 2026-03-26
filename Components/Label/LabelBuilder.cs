namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class LabelBuilder : ComponentBuilderBase<LabelComponent, LabelBuilder>
    {
        public LabelBuilder(LabelComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public LabelBuilder Text(string value) { this.Component.Text = value; return this; }
        public LabelBuilder CssClass(string value) { this.Component.CssClass = value; return this; }
        public LabelBuilder CssClassLabel(string value) { this.Component.CssClassLabel = value; return this; }
    }
}
