namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class HyperLinkHtmlBuilder : HtmlBuilderBase<HyperLinkComponent>
    {
        public HyperLinkHtmlBuilder(HyperLinkComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
