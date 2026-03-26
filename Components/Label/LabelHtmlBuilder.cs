namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label
{
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class LabelHtmlBuilder : HtmlBuilderBase<LabelComponent>
    {
        public LabelHtmlBuilder(LabelComponent component) { this.Component = component; }
        public override void Build(HtmlTextWriter writer) { }
    }
}
