namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Menu
{
    using System.Collections.Generic;

    public class MenuItem
    {
        public MenuItem()
        {
            ChildMenuItems = new List<MenuItem>();
        }

        public string Text { get; set; }
        public string Url { get; set; }
        public string Id { get; set; }
        public string CssClass { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
        public bool Hidden { get; set; }
        public EMenuCtrlType MenuType { get; set; }
        public List<MenuItem> ChildMenuItems { get; set; }
        
        public string ActionUrl { get; set; }
        public string MenuName { get; set; }
        public string OnclickEvent { get; set; }
        public string ActionName { get; set; }

        public List<MenuItem> ReturnChildMenu()
        {
            return ChildMenuItems;
        }
    }

    public enum EMenuCtrlType
    {
        None,
        Add,
        Edit,
        Delete,
        Export,
        Import,
        Print,
        Refresh,
        Search,
        View,
        Linkbutton,
        Redirect,
        RedirectJs,
        LinkbuttonJs,
        Imagebutton,
        ImagebuttonJs
    }
}
