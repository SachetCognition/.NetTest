namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropdownMenus
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class DropDownMenuHtmlBuilder : HtmlBuilderBase<DropDownMenuComponent>
    {
        public DropDownMenuHtmlBuilder(DropDownMenuComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
