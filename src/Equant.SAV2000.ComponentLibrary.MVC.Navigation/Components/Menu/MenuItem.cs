namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Menu
{
    using System.Collections.Generic;

    public enum EMenuCtrlType
    {
        Redirect = 0,
        RedirectJs = 1,
        Linkbutton = 2,
        LinkbuttonJs = 3,
        Imagebutton = 4,
        ImagebuttonJs = 5
    }

    public class MenuItem
    {
        public EMenuCtrlType MenuType { get; set; }
        public string Text { get; set; }
        public string MenuName { get; set; }
        public string ActionUrl { get; set; }
        public string ActionName { get; set; }
        public string CssClass { get; set; }
        public string AccessText { get; set; }
        public string AccessCss { get; set; }
        public string OnclickEvent { get; set; }
        public bool Hidden { get; set; }
        private List<MenuItem> _childMenus = new List<MenuItem>();

        public List<MenuItem> ReturnChildMenu()
        {
            return _childMenus;
        }
    }
}
