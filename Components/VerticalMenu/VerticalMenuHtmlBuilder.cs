namespace Equant.SAV2000.ComponentLibrary.MVC.Components.VerticalMenu
{
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class VerticalMenuHtmlBuilder : HtmlBuilderBase<VerticalMenuComponent>
    {
        public VerticalMenuHtmlBuilder(VerticalMenuComponent component) { this.Component = component; }
        public override void Build(HtmlTextWriter writer) { }
    }
}
