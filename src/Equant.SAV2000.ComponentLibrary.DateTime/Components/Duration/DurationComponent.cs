namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Duration
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

    public class DurationComponent : ComponentBase
    {
        public CustomLabelComponent CustomLabel { get; set; }
        public DurationEntity Value { get; set; }
        public string CssMainDiv { get; set; }
        public string CssClassInput { get; set; }
        public bool DisplaySeconds { get; set; }
        public bool DisplayMilliseconds { get; set; }

        public string HoursInputId => this.Id.AppendWithBuilder("TxtHours");
        public string MinutesInputId => this.Id.AppendWithBuilder("TxtMinutes");
        public string SecondsInputId => this.Id.AppendWithBuilder("TxtSeconds");
        public string MillisecondsInputId => this.Id.AppendWithBuilder("TxtMilliseconds");
        public string HoursInputName => !string.IsNullOrEmpty(this.Name) ? this.Name.AppendWithBuilder(".Hours") : this.Id.AppendWithBuilder(".Hours");
        public string MinutesInputName => !string.IsNullOrEmpty(this.Name) ? this.Name.AppendWithBuilder(".Minutes") : this.Id.AppendWithBuilder(".Minutes");
        public string SecondsInputName => !string.IsNullOrEmpty(this.Name) ? this.Name.AppendWithBuilder(".Seconds") : this.Id.AppendWithBuilder(".Seconds");
        public string MillisecondsInputName => !string.IsNullOrEmpty(this.Name) ? this.Name.AppendWithBuilder(".Milliseconds") : this.Id.AppendWithBuilder(".Milliseconds");
        public string MainDivId => this.Id.AppendWithBuilder("MainDiv");

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {
                var jsRes = new List<JsResource>
                {
                    new JsResource("JsDuration", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.Duration.js", 200,
                        typeof(DurationComponent))
                };
                return new ReadOnlyCollection<JsResource>(jsRes);
            }
        }

        public DurationComponent()
        {
            this.CustomLabel = new CustomLabelComponent();
            this.Value = new DurationEntity();
            this.DisplaySeconds = true;
            this.DisplayMilliseconds = true;
        }

        public override IHtmlContent RenderHtml()
        {
            return new DurationHtmlBuilder(this).Build();
        }

        public override string RenderInitScript()
        {
            if (!this.IsVisible) return string.Empty;

            var sb = new StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.Append("$(function(){$('#").Append(this.Id.JQuerySelectorEscape()).Append("').duration({");
            sb.Append("displaySeconds:").Append(this.DisplaySeconds ? "true" : "false");
            sb.Append(",displayMilliseconds:").Append(this.DisplayMilliseconds ? "true" : "false");
            sb.Append("});});");
            sb.AppendLine("</script>");

            return sb.ToString();
        }
    }
}
