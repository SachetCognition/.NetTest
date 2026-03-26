namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CommentBox
{
    using System.Text;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CommentBoxHtmlBuilder : HtmlBuilderBase<CommentBoxComponent>
    {
        public CommentBoxHtmlBuilder(CommentBoxComponent component) { this.Component = component; }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible) return HtmlString.Empty;

            var container = new TagBuilder("div");
            container.AddCssClass("comment-box");
            if (!string.IsNullOrEmpty(this.Component.CssClass))
                container.AddCssClass(this.Component.CssClass);

            var textarea = new TagBuilder("textarea");
            textarea.MergeAttribute("id", this.Component.Id);
            if (!string.IsNullOrEmpty(this.Component.Name))
                textarea.MergeAttribute("name", this.Component.Name);
            textarea.MergeAttribute("rows", this.Component.Rows.ToString());
            textarea.MergeAttribute("cols", this.Component.Cols.ToString());
            if (this.Component.MaxLength.HasValue)
                textarea.MergeAttribute("maxlength", this.Component.MaxLength.Value.ToString());
            if (this.Component.IsDisabled)
                textarea.MergeAttribute("disabled", "disabled");
            textarea.MergeAttributes(this.Component.HtmlAttributes);

            if (!string.IsNullOrEmpty(this.Component.Value))
                textarea.InnerHtml.Append(this.Component.Value);

            container.InnerHtml.AppendHtml(textarea);

            if (this.Component.ShowCharCount && this.Component.MaxLength.HasValue)
            {
                var charCount = new TagBuilder("span");
                charCount.MergeAttribute("id", this.Component.Id + "_charCount");
                charCount.AddCssClass("char-count");
                var remaining = this.Component.MaxLength.Value - (this.Component.Value?.Length ?? 0);
                charCount.InnerHtml.Append(remaining.ToString() + " characters remaining");
                container.InnerHtml.AppendHtml(charCount);
            }

            return container;
        }
    }
}
