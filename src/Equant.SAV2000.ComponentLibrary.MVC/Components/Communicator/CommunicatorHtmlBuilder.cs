namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Communicator
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CommunicatorHtmlBuilder : HtmlBuilderBase<CommunicatorComponent>
    {
        public CommunicatorHtmlBuilder(CommunicatorComponent component) : base(component) { }

        public override IHtmlContent Build()
        {
            var tagBuilder = new TagBuilder("div");

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

            tagBuilder.AddCssClass("communicator");

            foreach (var attr in Component.HtmlAttributes)
            {
                tagBuilder.MergeAttribute(attr.Key, attr.Value?.ToString());
            }

            if (!string.IsNullOrEmpty(Component.Channel))
            {
                tagBuilder.MergeAttribute("data-channel", Component.Channel);
            }

            if (!string.IsNullOrEmpty(Component.Text))
            {
                var innerSpan = new TagBuilder("span");
                innerSpan.AddCssClass("communicator-text");
                innerSpan.InnerHtml.Append(Component.Text);
                tagBuilder.InnerHtml.AppendHtml(innerSpan);
            }

            return tagBuilder;
        }
    }
}
