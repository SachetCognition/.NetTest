namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Web.Mvc;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class WeekYearComponent : ComponentBase
    {
        public WeekYearComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public WeekYearWithFormat Value { get; set; }
        public string WeekTextId { get; set; }
        public string YearTextId { get; set; }
        public string CssMainDiv { get; set; }
        public bool DisplayInformationIcon { get; set; }
        public string AssociatedWeekYearHtmlId { get; set; }
        public string CssClassWeekDiv { get; set; }
        public string OnWeekChange { get; set; }
        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new List<JsResource>()); }
        }
        public override void WriteHtml(HtmlTextWriter writer) { new WeekYearHtmlBuilder(this).Build(writer); }
        public override void WriteInitScript(HtmlTextWriter writer) { }
    }
}
