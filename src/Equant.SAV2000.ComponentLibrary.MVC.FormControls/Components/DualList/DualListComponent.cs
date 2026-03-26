namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DualList
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;

    public class DualListComponent : ComponentBase
    {
        public DualListComponent() : base()
        {
            this.AvailableItems = new DataCollection();
            this.SelectedItems = new DataCollection();
        }

        public DualListComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            this.AvailableItems = new DataCollection();
            this.SelectedItems = new DataCollection();
        }

        public string CssClass { get; set; }
        public DataCollection AvailableItems { get; set; }
        public DataCollection SelectedItems { get; set; }
        public int Size { get; set; } = 5;
        public string AvailableLabel { get; set; } = "Available";
        public string SelectedLabel { get; set; } = "Selected";

        public override IHtmlContent WriteHtml()
        {
            return new DualListHtmlBuilder(this).Build();
        }

        public override string WriteInitScript()
        {
            return string.Format("$('#{0}').savDualList();", this.Id);
        }
    }
}
