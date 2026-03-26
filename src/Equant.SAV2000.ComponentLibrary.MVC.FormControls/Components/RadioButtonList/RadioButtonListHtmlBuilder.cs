namespace Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButtonList
{
    using System.Text;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class RadioButtonListHtmlBuilder : HtmlBuilderBase<RadioButtonListComponent>
    {
        public RadioButtonListHtmlBuilder(RadioButtonListComponent component) { this.Component = component; }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible) return HtmlString.Empty;

            var container = new TagBuilder("ul");
            container.MergeAttribute("id", this.Component.Id);
            if (!string.IsNullOrEmpty(this.Component.CssClass))
                container.AddCssClass(this.Component.CssClass);
            if (this.Component.Layout == RadioButtonListLayout.Horizontal)
                container.AddCssClass("horizontal");

            container.MergeAttributes(this.Component.HtmlAttributes);

            var groupName = !string.IsNullOrEmpty(this.Component.GroupName) ? this.Component.GroupName : this.Component.Name;
            var sb = new StringBuilder();

            foreach (var item in this.Component.Items)
            {
                var li = new TagBuilder("li");
                var radio = new TagBuilder("input");
                var itemId = this.Component.Id + item.Value;
                radio.MergeAttribute("id", itemId);
                radio.MergeAttribute("type", "radio");
                radio.MergeAttribute("name", groupName);
                radio.MergeAttribute("value", item.Value);
                if (item.Value == this.Component.SelectedValue)
                    radio.MergeAttribute("checked", "checked");
                if (item.Disabled || this.Component.IsDisabled)
                    radio.MergeAttribute("disabled", "disabled");

                var label = new TagBuilder("label");
                label.MergeAttribute("for", itemId);
                label.InnerHtml.Append(item.Text);

                li.InnerHtml.AppendHtml(radio);
                li.InnerHtml.AppendHtml(label);
                sb.Append(li.ToHtmlString());
            }

            container.InnerHtml.AppendHtml(sb.ToString());
            return container;
        }
    }
}
