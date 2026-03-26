namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextArea
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class TextAreaHtmlBuilder : HtmlBuilderBase<TextAreaComponent>
    {
        public TextAreaHtmlBuilder(TextAreaComponent component) { this.Component = component; }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible) return HtmlString.Empty;

            var tag = new TagBuilder("textarea");
            tag.MergeAttribute("id", this.Component.Id);
            if (!string.IsNullOrEmpty(this.Component.Name))
                tag.MergeAttribute("name", this.Component.Name);
            tag.MergeAttribute("rows", this.Component.Rows.ToString());
            tag.MergeAttribute("cols", this.Component.Cols.ToString());
            if (this.Component.MaxLength.HasValue)
                tag.MergeAttribute("maxlength", this.Component.MaxLength.Value.ToString());
            if (this.Component.IsReadOnly)
                tag.MergeAttribute("readonly", "readonly");
            if (this.Component.IsDisabled)
                tag.MergeAttribute("disabled", "disabled");
            if (!string.IsNullOrEmpty(this.Component.CssClass))
                tag.AddCssClass(this.Component.CssClass);
            tag.MergeAttributes(this.Component.HtmlAttributes);

            if (!string.IsNullOrEmpty(this.Component.Value))
                tag.InnerHtml.Append(this.Component.Value);

            return tag;
        }
    }
}
