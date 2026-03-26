namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalTab
{
    using System.Collections.Generic;
    using System.IO;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HorizontalTabComponent : ComponentBase
    {
        public HorizontalTabComponent()
        {
            this.Tabs = new List<TabItem>();
        }

        public List<TabItem> Tabs { get; set; }
        public string ActiveTab { get; set; }
        public string CssClass { get; set; }
        public string OnTabChange { get; set; }

        public override void WriteHtml(TextWriter writer) { }
        public override void WriteInitScript(TextWriter writer) { }
    }

    public class TabItem
    {
        public string Text { get; set; }
        public string ContentId { get; set; }
        public string CssClass { get; set; }
        public bool IsActive { get; set; }
    }
}
