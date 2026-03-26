namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ListBox
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class ListBoxHtmlBuilder : HtmlBuilderBase<ListBoxComponent>
    {
        public ListBoxHtmlBuilder(ListBoxComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
