namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Menu
{
    using System.Collections.Generic;

    public class MenuItem
    {
        public MenuItem() { this.SubMenuItems = new List<MenuItem>(); }
        public string Text { get; set; }
        public string Url { get; set; }
        public string CssClass { get; set; }
        public bool IsSelected { get; set; }
        public List<MenuItem> SubMenuItems { get; set; }
    }
}
