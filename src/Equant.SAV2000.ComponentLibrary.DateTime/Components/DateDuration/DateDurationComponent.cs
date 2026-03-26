namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateDuration
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
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
    using Microsoft.AspNetCore.Html;

    public class DateDurationComponent : ComponentBase
    {
        public CustomLabelComponent CustomLabel { get; set; }
        public DateDuration Value { get; set; }
        public string Format { get; set; }
        public string CssMainDiv { get; set; }
        public string CssClassDateInput { get; set; }
        public string CssClassDurationInput { get; set; }
        public bool DisplayTime { get; set; }

        public string DateContainerId => this.Id.AppendWithBuilder("DateContainer");
        public string DurationContainerId => this.Id.AppendWithBuilder("DurationContainer");
        public string DurationDaysId => this.Id.AppendWithBuilder("DurationDays");
        public string DurationHoursId => this.Id.AppendWithBuilder("DurationHours");
        public string DurationMinutesId => this.Id.AppendWithBuilder("DurationMinutes");
        public string MainDivId => this.Id.AppendWithBuilder("MainDiv");

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {
                var jsRes = new List<JsResource>
                {
                    new JsResource("JsDateDuration", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DateDuration.js", 200,
                        typeof(DateDurationComponent))
                };
                return new ReadOnlyCollection<JsResource>(jsRes);
            }
        }

        public DateDurationComponent()
        {
            this.CustomLabel = new CustomLabelComponent();
            this.Value = new DateDuration();
            this.Format = DateTimeConstants.EnglishFormat;
            this.DisplayTime = false;
        }

        public override IHtmlContent RenderHtml()
        {
            return new DateDurationHtmlBuilder(this).Build();
        }

        public override string RenderInitScript()
        {
            if (!this.IsVisible) return string.Empty;

            var sb = new StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.Append("$(function(){$('#").Append(this.Id.JQuerySelectorEscape()).Append("').dateDuration({");
            sb.Append("dateContainer:'#").Append(this.DateContainerId.JQuerySelectorEscape()).Append("'");
            sb.Append(",durationContainer:'#").Append(this.DurationContainerId.JQuerySelectorEscape()).Append("'");
            sb.Append("});});");
            sb.AppendLine("</script>");

            return sb.ToString();
        }
    }
}
