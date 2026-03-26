namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Button
{
    using System;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ButtonHtmlBuilder : HtmlBuilderBase<ButtonComponent>
    {
        public ButtonHtmlBuilder(ButtonComponent component) { this.Component = component; }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible) return HtmlString.Empty;

            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("id", this.Component.Id);
            if (!string.IsNullOrEmpty(this.Component.Name))
                tagBuilder.MergeAttribute("name", this.Component.Name);
            tagBuilder.MergeAttribute("type", "submit");

            if (!string.IsNullOrWhiteSpace(this.Component.DialogDivId))
            {
                tagBuilder.MergeAttribute("data-toggle", "modal");
                tagBuilder.MergeAttribute("data-target", "#" + this.Component.DialogDivId);
            }

            tagBuilder.MergeAttributes(this.Component.HtmlAttributes);
            var effectiveCssClass = this.Component.IsDisabled ? this.Component.CssClassReadOnly : this.Component.CssClass;
            if (!string.IsNullOrEmpty(effectiveCssClass))
                tagBuilder.AddCssClass(effectiveCssClass);
            tagBuilder.TagRenderMode = TagRenderMode.StartTag;

            return tagBuilder;
        }
    }
}
