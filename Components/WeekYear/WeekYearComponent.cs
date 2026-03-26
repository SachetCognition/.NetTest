namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.Web.Mvc;

    public class WeekYearComponent : ComponentBase
    {
        public WeekYearComponent() { }
        public WeekYearComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }

        public WeekYearWithFormat Value { get; set; }
        public string WeekTextId { get; set; }

        public override void WriteHtml(System.Web.UI.HtmlTextWriter writer) { }
        public override void WriteInitScript(System.Web.UI.HtmlTextWriter writer) { }
    }
}
