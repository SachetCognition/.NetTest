namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Menu
{
    using System.Collections.Generic;

    public class MenuItem
    {
        public string Text { get; set; }
        public string Url { get; set; }
        public string CssClass { get; set; }
        public string Id { get; set; }
        public string AccessText { get; set; }
        public EMenuCtrlType MenuCtrlType { get; set; }
        public EMenuCtrlType MenuType { get; set; }
        public string MenuName { get; set; }
        public string ActionUrl { get; set; }
        public string ActionName { get; set; }
        public string OnclickEvent { get; set; }
        public bool Hidden { get; set; }
        private List<MenuItem> childMenuItems;

        public List<MenuItem> ReturnChildMenu()
        {
            return childMenuItems;
        }

        public void SetChildMenu(List<MenuItem> items)
        {
            childMenuItems = items;
        }
    }

    public enum EMenuCtrlType
    {
        Linkbutton,
        Redirect,
        Custom,
        RedirectJs,
        LinkbuttonJs,
        Imagebutton,
        ImagebuttonJs
    }
}
