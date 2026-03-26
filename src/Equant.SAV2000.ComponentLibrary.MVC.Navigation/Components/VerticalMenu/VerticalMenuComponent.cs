namespace Equant.SAV2000.ComponentLibrary.MVC.Components.VerticalMenu
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Image;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;

    public class VerticalMenuComponent : ComponentBase
    {
        public VerticalMenuComponent()
        {
            this.MenuItems = new List<MenuItem>();
            this.CopyRightImage = new ImageComponent();
        }
        public VerticalMenuComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            this.MenuItems = new List<MenuItem>();
            this.CopyRightImage = new ImageComponent(htmlHelper);
        }

        public List<VerticalMenuItemInfo> MenuItemsInformation { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public ImageComponent CopyRightImage { get; set; }
        public string CopyRightText { get; set; }
        public string SelectedMenu { get; set; }
        public bool CauseValidation { get; set; }
        public string CopyrightImageUrl { get; set; }
        public string CssClass { get; set; }
        public const string OuterMenuDivCssClass = "navbar";

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new List<JsResource>().AsReadOnly(); }
        }

        public override void WriteHtml(TextWriter writer) { }
        public override void WriteInitScript(TextWriter writer) { }
    }
}
