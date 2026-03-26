namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ProgressBar
{
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ProgressBarHtmlBuilder : HtmlBuilderBase<ProgressBarComponent>
    {
        public ProgressBarHtmlBuilder(ProgressBarComponent component)
        {
            this.Component = component;
        }

        public override void Build(HtmlTextWriter writer)
        {
        }
    }
}
