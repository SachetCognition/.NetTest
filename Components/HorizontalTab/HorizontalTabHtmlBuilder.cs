namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalTab
{
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HorizontalTabHtmlBuilder : HtmlBuilderBase<HorizontalTabComponent>
    {
        public HorizontalTabHtmlBuilder(HorizontalTabComponent component)
        {
            this.Component = component;
        }

        public override void Build(HtmlTextWriter writer)
        {
        }
    }
}
