namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ListBox
{
    using System.Linq;
    using System.Text;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ListBoxHtmlBuilder : HtmlBuilderBase<ListBoxComponent>
    {
        public ListBoxHtmlBuilder(ListBoxComponent component) { this.Component = component; }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible) return HtmlString.Empty;

            var tag = new TagBuilder("select");
            tag.MergeAttribute("id", this.Component.Id);
            if (!string.IsNullOrEmpty(this.Component.Name))
                tag.MergeAttribute("name", this.Component.Name);
            tag.MergeAttribute("multiple", "multiple");
            tag.MergeAttribute("size", this.Component.Size.ToString());
            if (this.Component.IsDisabled)
                tag.MergeAttribute("disabled", "disabled");
            if (!string.IsNullOrEmpty(this.Component.CssClass))
                tag.AddCssClass(this.Component.CssClass);
            tag.MergeAttributes(this.Component.HtmlAttributes);

            var sb = new StringBuilder();
            if (this.Component.DataSource != null)
            {
                foreach (var item in this.Component.DataSource.Items)
                {
                    var option = new TagBuilder("option");
                    option.MergeAttribute("value", item.Value);
                    if (item.Selected || (this.Component.SelectedValues != null && this.Component.SelectedValues.Contains(item.Value)))
                        option.MergeAttribute("selected", "selected");
                    option.InnerHtml.Append(item.Text);
                    sb.Append(option.ToHtmlString());
                }
            }

            tag.InnerHtml.AppendHtml(sb.ToString());
            return tag;
        }
    }
}
