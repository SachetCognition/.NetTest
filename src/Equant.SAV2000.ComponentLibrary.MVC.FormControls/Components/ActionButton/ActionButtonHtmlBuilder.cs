namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ActionButton
{
    using System.Text;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ActionButtonHtmlBuilder : HtmlBuilderBase<ActionButtonComponent>
    {
        public ActionButtonHtmlBuilder(ActionButtonComponent component) { this.Component = component; }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible) return HtmlString.Empty;

            var tbActionButton = new TagBuilder("button");
            tbActionButton.MergeAttribute("id", this.Component.Id);
            if (!string.IsNullOrEmpty(this.Component.Name))
                tbActionButton.MergeAttribute("name", this.Component.Name);

            if (!string.IsNullOrWhiteSpace(this.Component.DialogBoxId))
            {
                tbActionButton.MergeAttribute("data-toggle", "modal");
                tbActionButton.MergeAttribute("data-target", "#" + this.Component.DialogBoxId);
            }

            tbActionButton.MergeAttribute("type", "submit");
            tbActionButton.MergeAttributes(this.Component.HtmlAttributes);
            var effectiveCssClass = this.Component.IsDisabled ? this.Component.CssClassReadOnly : this.Component.CssClass;
            if (!string.IsNullOrEmpty(effectiveCssClass))
                tbActionButton.AddCssClass(effectiveCssClass);

            if (!string.IsNullOrEmpty(this.Component.AccessText))
            {
                var tbAccSpan = new TagBuilder("span");
                tbAccSpan.AddCssClass("hide-access");
                tbAccSpan.InnerHtml.Append(this.Component.AccessText);
                tbActionButton.InnerHtml.AppendHtml(tbAccSpan);
            }

            var tbTextSpan = new TagBuilder("span");
            if (!string.IsNullOrEmpty(this.Component.CssSpan))
                tbTextSpan.AddCssClass(this.Component.CssSpan);
            tbTextSpan.InnerHtml.Append(this.Component.Text);
            tbActionButton.InnerHtml.AppendHtml(tbTextSpan);

            return tbActionButton;
        }
    }
}
