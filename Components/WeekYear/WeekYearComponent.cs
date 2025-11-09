namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class WeekYearComponent : ComponentBase
    {
        public WeekYearComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
        }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new System.Collections.Generic.List<JsResource>()); }
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
        public string WeekText { get; set; }
        public string YearText { get; set; }
        public WeekFormat Format { get; set; }
        public double TimeOffset { get; set; }
        public bool IsUtcMode { get; set; }
    }

    public enum WeekFormat
    {
        English,
        French
    }
}
