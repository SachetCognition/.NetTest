namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBox
{
    using System.Globalization;
    using System.Text;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CheckBoxHtmlBuilder : HtmlBuilderBase<CheckBoxComponent>
    {
        public CheckBoxHtmlBuilder(CheckBoxComponent component) { this.Component = component; }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible) return HtmlString.Empty;

            var tagBuilderHidden = new TagBuilder("input");
            tagBuilderHidden.MergeAttribute("id", string.Format(CultureInfo.InvariantCulture, "{0}Hidden", this.Component.Id));
            if (!string.IsNullOrEmpty(this.Component.Name))
                tagBuilderHidden.MergeAttribute("name", this.Component.Name);
            tagBuilderHidden.MergeAttribute("type", "hidden");
            tagBuilderHidden.MergeAttribute("value", this.Component.IsChecked ? "true" : "false");

            var tagBuilderCheckBox = new TagBuilder("input");
            tagBuilderCheckBox.MergeAttribute("id", this.Component.Id);
            if (!string.IsNullOrEmpty(this.Component.Name))
                tagBuilderCheckBox.MergeAttribute("name", this.Component.Name);
            tagBuilderCheckBox.MergeAttribute("type", "checkbox");
            tagBuilderCheckBox.MergeAttribute("value", this.Component.IsChecked ? "true" : "false");
            tagBuilderCheckBox.MergeAttributes(this.Component.HtmlAttributes);

            if (this.Component.IsDisabled)
            {
                tagBuilderCheckBox.MergeAttribute("disabled", "disabled");
            }

            var effectiveCssClass = this.Component.IsDisabled ? this.Component.CssClassDisabled : this.Component.CssClass;
            if (!string.IsNullOrEmpty(effectiveCssClass))
                tagBuilderCheckBox.AddCssClass(effectiveCssClass);

            if (!string.IsNullOrEmpty(this.Component.Title))
                tagBuilderCheckBox.MergeAttribute("title", this.Component.Title);

            var sb = new StringBuilder();
            sb.Append(tagBuilderHidden.RenderStartTagString());
            sb.Append(tagBuilderCheckBox.RenderStartTagString());
            return new HtmlString(sb.ToString());
        }
    }
}
