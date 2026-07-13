namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Duration
{
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DurationHtmlBuilder : HtmlBuilderBase<DurationComponent>
    {
        public DurationHtmlBuilder(DurationComponent component)
        {
            this.Component = component;
        }

        public override void Build(HtmlTextWriter writer)
        {
        }
    }
}
