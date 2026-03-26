namespace Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButtonList
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class RadioButtonListComponent : ComponentBase
    {
        public RadioButtonListComponent() : base()
        {
            this.Items = new List<RadioButtonListItem>();
            this.Layout = RadioButtonListLayout.Vertical;
        }

        public RadioButtonListComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            this.Items = new List<RadioButtonListItem>();
            this.Layout = RadioButtonListLayout.Vertical;
        }

        public string CssClass { get; set; }
        public string GroupName { get; set; }
        public string SelectedValue { get; set; }
        public List<RadioButtonListItem> Items { get; set; }
        public RadioButtonListLayout Layout { get; set; }

        public override IHtmlContent WriteHtml()
        {
            return new RadioButtonListHtmlBuilder(this).Build();
        }

        public override string WriteInitScript() { return string.Empty; }
    }

    public class RadioButtonListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Disabled { get; set; }
    }

    public enum RadioButtonListLayout
    {
        Horizontal,
        Vertical
    }
}
