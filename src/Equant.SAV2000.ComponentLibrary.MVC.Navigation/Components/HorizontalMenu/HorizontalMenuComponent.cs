namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalMenu
{
    using System.Collections.Generic;
    using System.IO;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HorizontalMenuComponent : ComponentBase
    {
        public HorizontalMenuComponent()
        {
            this.MenuItems = new List<HorizontalMenuItemInfo>();
        }

        public List<HorizontalMenuItemInfo> MenuItems { get; set; }
        public string ActiveItem { get; set; }
        public string CssClass { get; set; }
        public string OnItemClick { get; set; }

        public override void WriteHtml(TextWriter writer) { }
        public override void WriteInitScript(TextWriter writer) { }
    }
}
