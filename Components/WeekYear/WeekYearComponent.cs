namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System;
    using System.Web.Mvc;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class WeekYearComponent : ComponentBase
    {
        public WeekYearComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public string CssClass { get; set; }
        public new WeekYearWithFormat Value { get; set; }
        public string WeekTextId { get { return this.Id + "_Week"; } }
        public override void WriteHtml(HtmlTextWriter writer) { }
        public override void WriteInitScript(HtmlTextWriter writer) { }
    }
}
