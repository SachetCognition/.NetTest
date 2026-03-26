namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DualList
{
    using System.Text;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DualListHtmlBuilder : HtmlBuilderBase<DualListComponent>
    {
        public DualListHtmlBuilder(DualListComponent component) { this.Component = component; }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible) return HtmlString.Empty;

            var container = new TagBuilder("div");
            container.MergeAttribute("id", this.Component.Id);
            if (!string.IsNullOrEmpty(this.Component.CssClass))
                container.AddCssClass(this.Component.CssClass);
            container.AddCssClass("dual-list");
            container.MergeAttributes(this.Component.HtmlAttributes);

            // Available list
            var availDiv = new TagBuilder("div");
            availDiv.AddCssClass("dual-list-available");
            var availLabel = new TagBuilder("label");
            availLabel.InnerHtml.Append(this.Component.AvailableLabel);
            var availSelect = BuildSelect(this.Component.Id + "_available", this.Component.AvailableItems);
            availDiv.InnerHtml.AppendHtml(availLabel);
            availDiv.InnerHtml.AppendHtml(availSelect);

            // Buttons
            var btnDiv = new TagBuilder("div");
            btnDiv.AddCssClass("dual-list-buttons");
            var addBtn = new TagBuilder("button");
            addBtn.MergeAttribute("type", "button");
            addBtn.AddCssClass("dual-list-add");
            addBtn.InnerHtml.Append(">");
            var removeBtn = new TagBuilder("button");
            removeBtn.MergeAttribute("type", "button");
            removeBtn.AddCssClass("dual-list-remove");
            removeBtn.InnerHtml.Append("<");
            btnDiv.InnerHtml.AppendHtml(addBtn);
            btnDiv.InnerHtml.AppendHtml(removeBtn);

            // Selected list
            var selDiv = new TagBuilder("div");
            selDiv.AddCssClass("dual-list-selected");
            var selLabel = new TagBuilder("label");
            selLabel.InnerHtml.Append(this.Component.SelectedLabel);
            var selSelect = BuildSelect(this.Component.Id + "_selected", this.Component.SelectedItems);
            if (!string.IsNullOrEmpty(this.Component.Name))
                selSelect.MergeAttribute("name", this.Component.Name, true);
            selDiv.InnerHtml.AppendHtml(selLabel);
            selDiv.InnerHtml.AppendHtml(selSelect);

            container.InnerHtml.AppendHtml(availDiv);
            container.InnerHtml.AppendHtml(btnDiv);
            container.InnerHtml.AppendHtml(selDiv);

            return container;
        }

        private TagBuilder BuildSelect(string id, DropDownList.DataCollection items)
        {
            var select = new TagBuilder("select");
            select.MergeAttribute("id", id);
            select.MergeAttribute("multiple", "multiple");
            select.MergeAttribute("size", this.Component.Size.ToString());
            if (this.Component.IsDisabled)
                select.MergeAttribute("disabled", "disabled");

            if (items != null)
            {
                foreach (var item in items.Items)
                {
                    var option = new TagBuilder("option");
                    option.MergeAttribute("value", item.Value);
                    option.InnerHtml.Append(item.Text);
                    select.InnerHtml.AppendHtml(option);
                }
            }

            return select;
        }
    }
}
