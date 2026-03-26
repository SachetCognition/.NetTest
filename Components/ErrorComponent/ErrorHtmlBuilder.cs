namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent
{
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ErrorHtmlBuilder : HtmlBuilderBase<ErrorComponents>
    {
        public ErrorHtmlBuilder(ErrorComponents component) { this.Component = component; }
        public override void Build(HtmlTextWriter writer) { }
    }
}
