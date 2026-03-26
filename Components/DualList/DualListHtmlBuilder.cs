namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DualList
{
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DualListHtmlBuilder : HtmlBuilderBase<DualListComponent>
    {
        public DualListHtmlBuilder(DualListComponent component)
        {
            this.Component = component;
        }

        public override void Build(HtmlTextWriter writer)
        {
        }
    }
}
