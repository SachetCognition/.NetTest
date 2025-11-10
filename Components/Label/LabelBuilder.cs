namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class LabelBuilder : ComponentBuilderBase<LabelComponent, LabelBuilder>
    {
        public LabelBuilder(LabelComponent component, ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public LabelBuilder Text(string value)
        {
            Component.Text = value;
            return this;
        }

        public LabelBuilder For(string value)
        {
            Component.For = value;
            return this;
        }

        public LabelBuilder CssClass(string value)
        {
            Component.CssClass = value;
            return this;
        }

        /// <summary>
        /// </summary>
        public LabelBuilder HtmlAttributes(object htmlAttributes)
        {
            if (htmlAttributes != null)
            {
                var properties = htmlAttributes.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    Component.HtmlAttributes[prop.Name] = prop.GetValue(htmlAttributes);
                }
            }
            return this;
        }

        /// <summary>
        /// </summary>
        public LabelBuilder CssClassLabel(string cssClass)
        {
            Component.CssClassLabel = cssClass;
            return this;
        }
    }
}
