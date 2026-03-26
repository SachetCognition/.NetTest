namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList
{
    using System.Text;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DropDownListHtmlBuilder : HtmlBuilderBase<DropDownListComponent>
    {
        public DropDownListHtmlBuilder(DropDownListComponent component) { this.Component = component; }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible) return HtmlString.Empty;

            var tag = new TagBuilder("select");
            tag.MergeAttribute("id", this.Component.Id);
            if (!string.IsNullOrEmpty(this.Component.Name))
                tag.MergeAttribute("name", this.Component.Name);
            if (this.Component.IsDisabled)
                tag.MergeAttribute("disabled", "disabled");
            var effectiveCssClass = this.Component.IsDisabled ? this.Component.CssClassReadOnly : this.Component.CssClass;
            if (!string.IsNullOrEmpty(effectiveCssClass))
                tag.AddCssClass(effectiveCssClass);
            if (!string.IsNullOrEmpty(this.Component.CascadeFrom))
                tag.MergeAttribute("data-cascade-from", this.Component.CascadeFrom);

            tag.MergeAttributes(this.Component.HtmlAttributes);

            var optionsHtml = new StringBuilder();

            if (!string.IsNullOrEmpty(this.Component.PlaceholderText))
            {
                var placeholder = new TagBuilder("option");
                placeholder.MergeAttribute("value", "");
                placeholder.InnerHtml.Append(this.Component.PlaceholderText);
                optionsHtml.Append(placeholder.ToHtmlString());
            }

            if (this.Component.DataSource != null)
            {
                foreach (var item in this.Component.DataSource.Items)
                {
                    var option = new TagBuilder("option");
                    option.MergeAttribute("value", item.Value);
                    if (item.Selected || item.Value == this.Component.SelectedValue)
                        option.MergeAttribute("selected", "selected");
                    if (item.Disabled)
                        option.MergeAttribute("disabled", "disabled");
                    option.InnerHtml.Append(item.Text);
                    optionsHtml.Append(option.ToHtmlString());
                }
            }

            tag.InnerHtml.AppendHtml(optionsHtml.ToString());
            return tag;
        }
    }
}
