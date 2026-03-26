namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class DropDownListHtmlBuilder : HtmlBuilderBase<DropDownListComponent>
    {
        public DropDownListHtmlBuilder(DropDownListComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
