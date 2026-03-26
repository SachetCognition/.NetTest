namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomHeader
{
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CustomHeaderHtmlBuilder : HtmlBuilderBase<CustomHeaderComponent>
    {
        public CustomHeaderHtmlBuilder(CustomHeaderComponent component)
        {
            this.Component = component;
        }

        public override void Build(HtmlTextWriter writer)
        {
        }
    }
}
