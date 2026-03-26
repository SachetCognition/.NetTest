namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class LabelBuilder : ComponentBuilderBase<LabelComponent, LabelBuilder>
    {
        public LabelBuilder(LabelComponent component) : base(component) { }
        public LabelBuilder(LabelComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public LabelBuilder Text(string text) { return this; }
        public LabelBuilder CssClassLabel(string cssClass) { return this; }
        public LabelBuilder HtmlAttributes(object attributes) { return this; }
    }
}
