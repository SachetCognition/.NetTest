namespace Equant.SAV2000.ComponentLibrary.MVC.Components.BreadCrumbs
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class BreadCrumbsHtmlBuilder : HtmlBuilderBase<BreadCrumbsComponent>
    {
        public BreadCrumbsHtmlBuilder(BreadCrumbsComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
