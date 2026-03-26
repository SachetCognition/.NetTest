namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalMenu
{
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HorizontalMenuHtmlBuilder : HtmlBuilderBase<HorizontalMenuComponent>
    {
        public HorizontalMenuHtmlBuilder(HorizontalMenuComponent component)
        {
            this.Component = component;
        }

        public override void Build(HtmlTextWriter writer)
        {
        }
    }
}
