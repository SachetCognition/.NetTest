namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid
{
    using System.Collections.Generic;

    public class TreeGridimage
    {
        public string Source { get; set; }
        public string Title { get; set; }
        public string ActionUrl { get; set; }
        public bool IsVisible { get; set; } = true;
        public Dictionary<string, object> HtmlAttributes { get; set; } = new Dictionary<string, object>();
    }
}
