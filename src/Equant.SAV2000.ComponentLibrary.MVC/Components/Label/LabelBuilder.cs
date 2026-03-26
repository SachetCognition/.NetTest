namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class LabelBuilder : ComponentBuilderBase<LabelComponent, LabelBuilder>
    {
        public LabelBuilder(LabelComponent component) : base(component) { }
        public LabelBuilder(LabelComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public LabelBuilder Text(string text) { Component.Text = text; return this; }
        public LabelBuilder CssClassLabel(string cssClass) { Component.CssClass = cssClass; return this; }
        public LabelBuilder HtmlAttributes(object attributes)
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
    }
}
