namespace Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButton
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class RadioButtonHtmlBuilder : HtmlBuilderBase<RadioButtonComponent>
    {
        public RadioButtonHtmlBuilder(RadioButtonComponent component) { this.Component = component; }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible) return HtmlString.Empty;

            var tag = new TagBuilder("input");
            tag.MergeAttribute("id", this.Component.Id);
            tag.MergeAttribute("type", "radio");
            if (!string.IsNullOrEmpty(this.Component.GroupName))
                tag.MergeAttribute("name", this.Component.GroupName);
            else if (!string.IsNullOrEmpty(this.Component.Name))
                tag.MergeAttribute("name", this.Component.Name);
            if (!string.IsNullOrEmpty(this.Component.Value))
                tag.MergeAttribute("value", this.Component.Value);
            if (this.Component.IsChecked)
                tag.MergeAttribute("checked", "checked");
            if (this.Component.IsDisabled)
                tag.MergeAttribute("disabled", "disabled");
            if (!string.IsNullOrEmpty(this.Component.CssClass))
                tag.AddCssClass(this.Component.CssClass);
            tag.MergeAttributes(this.Component.HtmlAttributes);
            tag.TagRenderMode = TagRenderMode.SelfClosing;
            return tag;
        }
    }
}
