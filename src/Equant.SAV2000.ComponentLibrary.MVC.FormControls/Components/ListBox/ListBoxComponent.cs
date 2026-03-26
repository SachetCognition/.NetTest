namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ListBox
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;

    public class ListBoxComponent : ComponentBase
    {
        public ListBoxComponent() : base()
        {
            this.DataSource = new DataCollection();
            this.Size = 4;
        }

        public ListBoxComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            this.DataSource = new DataCollection();
            this.Size = 4;
        }

        public string CssClass { get; set; }
        public DataCollection DataSource { get; set; }
        public int Size { get; set; }
        public List<string> SelectedValues { get; set; } = new List<string>();

        public override IHtmlContent WriteHtml()
        {
            return new ListBoxHtmlBuilder(this).Build();
        }

        public override string WriteInitScript() { return string.Empty; }
    }
}
