namespace Equant.SAV2000.ComponentLibrary.MVC.Components.PopOver
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class PopOverHtmlBuilder : HtmlBuilderBase<PopOverComponent>
    {
        public PopOverHtmlBuilder(PopOverComponent component) : base(component) { }

        public override IHtmlContent Build()
        {
            var tagBuilder = new TagBuilder("span");

            if (!string.IsNullOrEmpty(Component.Id))
            {
                tagBuilder.MergeAttribute("id", Component.Id);
            }

            if (!string.IsNullOrEmpty(Component.CssClass))
            {
                tagBuilder.AddCssClass(Component.CssClass);
            }

            foreach (var attr in Component.HtmlAttributes)
            {
                tagBuilder.MergeAttribute(attr.Key, attr.Value?.ToString());
            }

            // Add popover data attributes
            tagBuilder.MergeAttribute("data-toggle", "popover");

            if (!string.IsNullOrEmpty(Component.Title))
            {
                tagBuilder.MergeAttribute("data-title", Component.Title);
            }

            if (!string.IsNullOrEmpty(Component.Text))
            {
                tagBuilder.MergeAttribute("data-content", Component.Text);
            }

            if (!string.IsNullOrEmpty(Component.Placement))
            {
                tagBuilder.MergeAttribute("data-placement", Component.Placement);
            }

            if (!string.IsNullOrEmpty(Component.TriggerElement))
            {
                tagBuilder.InnerHtml.AppendHtml(Component.TriggerElement);
            }

            return tagBuilder;
        }
    }
}
