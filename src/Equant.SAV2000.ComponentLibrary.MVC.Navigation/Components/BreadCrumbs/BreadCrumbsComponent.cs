namespace Equant.SAV2000.ComponentLibrary.MVC.Components.BreadCrumbs
{
    using System.Collections.Generic;
    using System.IO;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class BreadCrumbsComponent : ComponentBase
    {
        public BreadCrumbsComponent()
        {
            this.Items = new List<BreadCrumbsItem>();
        }

        public List<BreadCrumbsItem> Items { get; set; }
        public string Separator { get; set; } = " &gt; ";
        public string CssClass { get; set; }

        public override void WriteHtml(TextWriter writer) { }
        public override void WriteInitScript(TextWriter writer) { }
    }
}
