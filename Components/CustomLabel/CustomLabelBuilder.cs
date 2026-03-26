namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CustomLabelBuilder : ComponentBuilderBase<CustomLabelComponent, CustomLabelBuilder>
    {
        public CustomLabelBuilder(CustomLabelComponent component) : base(component) { }
        public CustomLabelBuilder(CustomLabelComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public CustomLabelBuilder Text(string text) { return this; }
        public CustomLabelBuilder AssociatedControlId(string id) { Component.AssociatedControlId = id; return this; }
        public CustomLabelBuilder HtmlAttributes(object attributes) { return this; }
        public CustomLabelBuilder IsOnlyForAccess(bool value) { return this; }
        public CustomLabelBuilder IsMandatory(bool value) { return this; }
        public CustomLabelBuilder CssClassLabel(string cssClass) { return this; }
    }
}
