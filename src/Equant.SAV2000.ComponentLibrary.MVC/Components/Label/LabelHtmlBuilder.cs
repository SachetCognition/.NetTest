namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class LabelHtmlBuilder : HtmlBuilderBase<LabelComponent>
    {
        public LabelHtmlBuilder(LabelComponent component) : base(component) { }

        public override IHtmlContent Build()
        {
            var tagBuilder = new TagBuilder("label");

            if (!string.IsNullOrEmpty(Component.Id))
            {
                tagBuilder.MergeAttribute("id", Component.Id);
            }

            if (!string.IsNullOrEmpty(Component.Name))
            {
                tagBuilder.MergeAttribute("name", Component.Name);
            }

            if (!string.IsNullOrEmpty(Component.CssClass))
            {
                tagBuilder.AddCssClass(Component.CssClass);
            }

            foreach (var attr in Component.HtmlAttributes)
            {
                tagBuilder.MergeAttribute(attr.Key, attr.Value?.ToString());
            }

            if (!string.IsNullOrEmpty(Component.Text))
            {
                tagBuilder.InnerHtml.Append(Component.Text);
            }

            return tagBuilder;
        }
    }
}
