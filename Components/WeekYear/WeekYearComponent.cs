namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public class WeekYearComponent : ComponentBase
    {
        public WeekYearComponent() { }
        public WeekYearComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public WeekYearWithFormat Value { get; set; }
        public string WeekTextId { get; set; }

        public override void WriteHtml(System.IO.TextWriter writer) { }
        public override void WriteInitScript(System.IO.TextWriter writer) { }
    }
}
