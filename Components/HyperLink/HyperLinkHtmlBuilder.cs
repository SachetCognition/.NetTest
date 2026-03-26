namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink
{
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HyperLinkHtmlBuilder : HtmlBuilderBase<HyperLinkComponent>
    {
        public HyperLinkHtmlBuilder(HyperLinkComponent component) : base(component) { }
        public override void Build(HtmlTextWriter writer) { }
    }
}
