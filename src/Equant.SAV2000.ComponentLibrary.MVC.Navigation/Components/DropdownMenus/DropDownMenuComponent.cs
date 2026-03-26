namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropdownMenus
{
    using System.Collections.Generic;
    using System.IO;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DropDownMenuComponent : ComponentBase
    {
        public DropDownMenuComponent()
        {
            this.MenuItems = new List<DropdownMenu>();
        }

        public List<DropdownMenu> MenuItems { get; set; }
        public string CssClass { get; set; }
        public string OnItemClick { get; set; }

        public override void WriteHtml(TextWriter writer) { }
        public override void WriteInitScript(TextWriter writer) { }
    }
}
