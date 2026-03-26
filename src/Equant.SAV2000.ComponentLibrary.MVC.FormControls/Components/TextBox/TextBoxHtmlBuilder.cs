namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextBox
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class TextBoxHtmlBuilder : HtmlBuilderBase<TextBoxComponent>
    {
        public TextBoxHtmlBuilder(TextBoxComponent component) { this.Component = component; }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible) return HtmlString.Empty;

            var tag = new TagBuilder("input");
            tag.MergeAttribute("id", this.Component.Id);
            if (!string.IsNullOrEmpty(this.Component.Name))
                tag.MergeAttribute("name", this.Component.Name);
            tag.MergeAttribute("type", "text");

            if (!string.IsNullOrEmpty(this.Component.Value))
                tag.MergeAttribute("value", this.Component.Value);
            if (this.Component.MaxLength.HasValue)
                tag.MergeAttribute("maxlength", this.Component.MaxLength.Value.ToString());
            if (!string.IsNullOrEmpty(this.Component.Placeholder))
                tag.MergeAttribute("placeholder", this.Component.Placeholder);

            tag.MergeAttributes(this.Component.HtmlAttributes);

            if (this.Component.IsReadOnly)
                tag.MergeAttribute("readonly", "readonly");
            if (this.Component.IsDisabled)
                tag.MergeAttribute("disabled", "disabled");

            tag.AddCssClass((this.Component.IsReadOnly || this.Component.IsDisabled)
                ? this.Component.CssClassReadOnly
                : this.Component.CssClass);

            tag.TagRenderMode = TagRenderMode.SelfClosing;
            return tag;
        }
    }
}
