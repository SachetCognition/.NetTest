namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Text;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
    using Microsoft.AspNetCore.Html;

    public class WeekYearComponent : ComponentBase
    {
        public CustomLabelComponent CustomLabel { get; set; }
        public WeekYearWithFormat Value { get; set; }
        public string CssMainDiv { get; set; }
        public string CssClassWeekInput { get; set; }
        public string CssClassYearInput { get; set; }
        public string OnWeekChange { get; set; }

        public string WeekInputId => this.Id.AppendWithBuilder("TxtWeek");
        public string YearInputId => this.Id.AppendWithBuilder("TxtYear");
        public string WeekInputName => !string.IsNullOrEmpty(this.Name) ? this.Name.AppendWithBuilder(".Week") : this.Id.AppendWithBuilder(".Week");
        public string YearInputName => !string.IsNullOrEmpty(this.Name) ? this.Name.AppendWithBuilder(".Year") : this.Id.AppendWithBuilder(".Year");
        public string FormatHiddenName => !string.IsNullOrEmpty(this.Name) ? this.Name.AppendWithBuilder(".Format") : this.Id.AppendWithBuilder(".Format");
        public string MainDivId => this.Id.AppendWithBuilder("MainDiv");

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {
                var jsRes = new List<JsResource>
                {
                    new JsResource("JsWeekYear", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.WeekYear.js", 200,
                        typeof(WeekYearComponent))
                };
                return new ReadOnlyCollection<JsResource>(jsRes);
            }
        }

        public WeekYearComponent()
        {
            this.CustomLabel = new CustomLabelComponent();
            this.Value = new WeekYearWithFormat();
            this.OnWeekChange = "null";
        }

        public override IHtmlContent RenderHtml()
        {
            return new WeekYearHtmlBuilder(this).Build();
        }

        public override string RenderInitScript()
        {
            if (!this.IsVisible) return string.Empty;

            var sb = new StringBuilder();
            var weekId = this.WeekInputId.JQuerySelectorEscape();

            sb.AppendLine("<script type=\"text/javascript\">");
            sb.Append("$(function(){$('#").Append(weekId).Append("').weekYear({");
            sb.Append("yearId:'#").Append(this.YearInputId.JQuerySelectorEscape()).Append("'");
            sb.Append(",onWeekChange:").Append(this.OnWeekChange);
            sb.Append("});});");
            sb.AppendLine("</script>");

            return sb.ToString();
        }
    }
}
