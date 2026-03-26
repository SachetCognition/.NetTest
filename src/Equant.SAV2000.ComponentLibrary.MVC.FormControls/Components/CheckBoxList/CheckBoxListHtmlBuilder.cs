namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBoxList
{
    using System;
    using System.Text;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

    public class CheckBoxListHtmlBuilder : HtmlBuilderBase<CheckBoxListComponent>
    {
        public CheckBoxListHtmlBuilder(CheckBoxListComponent component) { this.Component = component; }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible) return HtmlString.Empty;

            var controlStringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(Component.CheckBoxListLabel.Text))
            {
                controlStringBuilder.Append(this.Component.CheckBoxListLabel.ToHtmlString());
            }

            var tagBuilderFieldSet = new TagBuilder("fieldset");
            if (!string.IsNullOrEmpty(this.Component.CssClassFieldSet))
                tagBuilderFieldSet.AddCssClass(this.Component.CssClassFieldSet);

            var tagBuilderUl = new TagBuilder("ul");
            if (!string.IsNullOrEmpty(this.Component.CssClass))
                tagBuilderUl.AddCssClass("checkboxpadding " + this.Component.CssClass);
            tagBuilderUl.MergeAttribute("id", this.Component.Id);
            tagBuilderUl.MergeAttributes(this.Component.HtmlAttributes);

            if (this.Component.IsDisabled)
            {
                this.Component.CssClass = this.Component.CssClassDisabled;
                tagBuilderFieldSet.MergeAttribute("disabled", "disabled");
            }

            var liStringBuilder = new StringBuilder();
            foreach (var item in this.Component.SourceItems)
            {
                var tagBuilderLi = new TagBuilder("li");
                if (!string.IsNullOrEmpty(item.Text))
                {
                    var chkBoxBuilder = new StringBuilder();
                    GenerateCheckBox(item, chkBoxBuilder);
                    tagBuilderLi.InnerHtml.AppendHtml(chkBoxBuilder.ToString());
                }
                liStringBuilder.Append(tagBuilderLi.ToHtmlString());
            }

            if (liStringBuilder.Length == 0)
            {
                var emptyLi = new TagBuilder("li");
                liStringBuilder.Append(emptyLi.ToHtmlString());
            }

            tagBuilderUl.InnerHtml.AppendHtml(liStringBuilder.ToString());
            if (!string.IsNullOrEmpty(this.Component.Title))
                tagBuilderUl.MergeAttribute("title", this.Component.Title);

            var legend = new TagBuilder("legend");
            legend.MergeAttribute("class", "hide-access");
            var hiddenSpan = new TagBuilder("span");
            hiddenSpan.InnerHtml.Append(this.Component.Title ?? string.Empty);
            legend.InnerHtml.AppendHtml(hiddenSpan);

            var div = new TagBuilder("div");
            var divCssClass = string.IsNullOrEmpty(this.Component.CssClassCheckBoxDiv) ? "checkbox-list-scroll" : "checkbox-list-scroll " + this.Component.CssClassCheckBoxDiv;
            div.MergeAttribute("class", divCssClass);

            var innerHtml = new StringBuilder();
            innerHtml.Append(legend.ToHtmlString());
            innerHtml.Append(div.RenderStartTagString());
            innerHtml.Append(tagBuilderUl.ToHtmlString());
            innerHtml.Append(div.RenderEndTagString());

            tagBuilderFieldSet.InnerHtml.AppendHtml(innerHtml.ToString());

            if (this.Component.IsOuterDivNeeded)
            {
                var tagBuilderOuterDiv = new TagBuilder("div");
                if (!string.IsNullOrEmpty(this.Component.CssClassOuterDiv))
                    tagBuilderOuterDiv.AddCssClass(this.Component.CssClassOuterDiv);
                controlStringBuilder.Append(tagBuilderOuterDiv.RenderStartTagString());
                controlStringBuilder.Append(tagBuilderFieldSet.ToHtmlString());
                controlStringBuilder.Append(tagBuilderOuterDiv.RenderEndTagString());
            }
            else
            {
                controlStringBuilder.Append(tagBuilderFieldSet.ToHtmlString());
            }

            return new HtmlString(controlStringBuilder.ToString());
        }

        private void GenerateCheckBox(CheckBoxListItem item, StringBuilder chkBoxBuilder)
        {
            var compId = this.Component.Id + item.Value;
            var tagBuilderCheckBox = new TagBuilder("input");
            tagBuilderCheckBox.MergeAttribute("id", compId);
            tagBuilderCheckBox.MergeAttribute("type", "checkbox");
            tagBuilderCheckBox.MergeAttribute("value", item.Value);
            if (!string.IsNullOrEmpty(this.Component.Name))
                tagBuilderCheckBox.MergeAttribute("name", this.Component.Name);
            if (item.Disabled)
            {
                this.Component.CssClass = this.Component.CssClassDisabled;
                tagBuilderCheckBox.MergeAttribute("disabled", "disabled");
            }
            if (item.Selected)
                tagBuilderCheckBox.MergeAttribute("checked", "checked");

            var tagBuilderLabel = new TagBuilder("label");
            tagBuilderLabel.MergeAttribute("for", compId);
            tagBuilderLabel.InnerHtml.Append(item.Text);

            chkBoxBuilder.Append(tagBuilderCheckBox.RenderStartTagString());
            chkBoxBuilder.Append(tagBuilderLabel.ToHtmlString());
        }
    }
}
