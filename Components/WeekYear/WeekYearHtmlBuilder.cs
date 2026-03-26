namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class WeekYearHtmlBuilder : HtmlBuilderBase<WeekYearComponent>
    {
        public WeekYearHtmlBuilder(WeekYearComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
