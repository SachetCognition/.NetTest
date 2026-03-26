namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class LabelBuilder : ComponentBuilderBase<LabelComponent, LabelBuilder>
    {
        public LabelBuilder(LabelComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }
        public LabelBuilder Text(string value) { Component.Text = value; return this; }
        public LabelBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public LabelBuilder CssClassLabel(string value) { Component.CssClassLabel = value; return this; }
        public LabelBuilder For(string value) { Component.For = value; return this; }
    }
}
