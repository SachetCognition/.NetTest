namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;

    public class WeekYearComponent : ComponentBase
    {
        public WeekYearComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
        }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new System.Collections.Generic.List<JsResource>()); }
        }

        public bool Disabled { get; set; }
        public WeekYearWithFormat Value { get; set; } = new WeekYearWithFormat();
        public string WeekTextId { get; set; } = string.Empty;
        public bool DisplayInformationIcon { get; set; }
        public string CssMainDiv { get; set; }
        public string CssClass { get; set; }
        public string AssociatedWeekYearHtmlId { get; set; }
        
        public System.Collections.Generic.List<MenuItem> ReturnChildMenu()
        {
            return new System.Collections.Generic.List<MenuItem>();
        }

        public string ToHtmlString()
        {
            using (var writer = new StringWriter())
            {
                WriteHtml(writer);
                return writer.ToString();
            }
        }

        public override void WriteHtml(TextWriter writer)
        {
            if (IsVisible)
            {
                writer.Write($"<div id=\"{Id}\"></div>");
            }
        }

        public override void WriteInitScript(TextWriter writer)
        {
        }
    }

    public class WeekAndYear
    {
        public int Week { get; set; }
        public int Year { get; set; }
    }

    public class WeekYearWithFormat
    {
        public string WeekText { get; set; } = string.Empty;
        public string YearText { get; set; } = string.Empty;
        public WeekFormat Format { get; set; }
        public double TimeOffset { get; set; }
        public bool IsUtcMode { get; set; }
        public System.DateTime Date { get; set; }
    }

    public enum WeekFormat
    {
        English,
        French
    }
}
