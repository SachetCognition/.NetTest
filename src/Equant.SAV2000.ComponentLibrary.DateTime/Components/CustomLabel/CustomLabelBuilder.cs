namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel
{
    using System;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class CustomLabelBuilder : ComponentBuilderBase<CustomLabelComponent, CustomLabelBuilder>
    {
        public CustomLabelBuilder(CustomLabelComponent component) : base(component) { }
        public CustomLabelBuilder(CustomLabelComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public CustomLabelBuilder Text(string text) { Component.Text = text; return this; }
        public CustomLabelBuilder AssociatedControlId(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            Component.AssociatedControlId = id;
            return this;
        }
        public CustomLabelBuilder HtmlAttributes(object attributes)
        {
            if (attributes != null)
            {
                foreach (var prop in attributes.GetType().GetProperties())
                {
                    Component.HtmlAttributes[prop.Name] = prop.GetValue(attributes);
                }
            }
            return this;
        }
        public CustomLabelBuilder IsOnlyForAccess(bool value) { Component.IsOnlyForAccess = value; return this; }
        public CustomLabelBuilder IsMandatory(bool value) { Component.IsMandatory = value; return this; }
        public CustomLabelBuilder CssClassLabel(string cssClass) { Component.CssClassLabel = cssClass; return this; }
        public CustomLabelBuilder DisplayColon(bool value) { Component.DisplayColon = value; return this; }
    }
}
