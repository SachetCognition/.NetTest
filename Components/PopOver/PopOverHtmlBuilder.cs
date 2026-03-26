namespace Equant.SAV2000.ComponentLibrary.MVC.Components.PopOver
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class PopOverHtmlBuilder : HtmlBuilderBase<PopOverComponent>
    {
        public PopOverHtmlBuilder(PopOverComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
