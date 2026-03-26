namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CustomLabelBuilder : ComponentBuilderBase<CustomLabelComponent, CustomLabelBuilder>
    {
        public CustomLabelBuilder(CustomLabelComponent component) : base(component) { }
        public CustomLabelBuilder(CustomLabelComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public CustomLabelBuilder Text(string text) { Component.Text = text; return this; }
        public CustomLabelBuilder AssociatedControlId(string id) { if (string.IsNullOrEmpty(id)) throw new System.ArgumentNullException(nameof(id)); Component.AssociatedControlId = id; return this; }
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
        public CustomLabelBuilder SuperScriptText(string value) { Component.SuperscriptText = value; return this; }
        public CustomLabelBuilder SuperscriptCssClass(string value) { Component.SuperscriptCssClass = value; return this; }
        public CustomLabelBuilder SuperscriptToolTip(string value) { Component.SuperscriptToolTip = value; return this; }
        public CustomLabelBuilder DisplayStar(bool value) { Component.DisplayStar = value; return this; }
        public CustomLabelBuilder DisplayColon(bool value) { Component.DisplayColon = value; return this; }
        public CustomLabelBuilder IsHtmlEncode(bool value) { Component.IsHtmlEncode = value; return this; }
    }
}
