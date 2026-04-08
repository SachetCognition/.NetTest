namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropdownMenus
{
    using System.Collections.Generic;

    public class DropdownMenu
    {
        public DropdownMenu()
        {
            this.Children = new List<ChildMenu>();
        }

        public string Text { get; set; }
        public string Url { get; set; }
        public string CssClass { get; set; }
        public List<ChildMenu> Children { get; set; }
    }
}
