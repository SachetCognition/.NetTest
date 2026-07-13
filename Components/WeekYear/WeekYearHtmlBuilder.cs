namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class WeekYearHtmlBuilder : HtmlBuilderBase<WeekYearComponent>
    {
        public WeekYearHtmlBuilder(WeekYearComponent component) { this.Component = component; }
        public override void Build(HtmlTextWriter writer) { }
    }
}
