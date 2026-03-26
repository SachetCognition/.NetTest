namespace Equant.SAV2000.ComponentLibrary.MVC.Components.BreadCrumbs
{
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class BreadCrumbsHtmlBuilder : HtmlBuilderBase<BreadCrumbsComponent>
    {
        public BreadCrumbsHtmlBuilder(BreadCrumbsComponent component) : base(component) { }
        public override void Build(HtmlTextWriter writer) { }
    }
}
