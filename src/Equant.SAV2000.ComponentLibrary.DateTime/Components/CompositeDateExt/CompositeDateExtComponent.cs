namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDateExt
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Text;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
    using Microsoft.AspNetCore.Html;

    public class CompositeDateExtComponent : ComponentBase
    {
        public CustomLabelComponent CustomLabel { get; set; }
        public CompositeDateExtViewModel ViewModel { get; set; }
        public string Format { get; set; }
        public string OnDateTypeChange { get; set; }
        public string CssMainDiv { get; set; }
        public string CssClassDateDiv { get; set; }
        public string CssClassDateInput { get; set; }
        public string CssClassDropDown { get; set; }
        public bool DisplayTime { get; set; }
        public List<EnumDateTypes> AvailableDateTypes { get; set; }
        public List<DateExtSelectionDropDown> DateTypeItems { get; set; }

        public string DateTypeDropDownId => this.Id.AppendWithBuilder("DdlDateType");
        public string DateFromContainerId => this.Id.AppendWithBuilder("DateFromContainer");
        public string DateToContainerId => this.Id.AppendWithBuilder("DateToContainer");
        public string WeekFromContainerId => this.Id.AppendWithBuilder("WeekFromContainer");
        public string WeekToContainerId => this.Id.AppendWithBuilder("WeekToContainer");
        public string ModelContainerId => this.Id.AppendWithBuilder("ModelContainer");
        public string DepthAddId => this.Id.AppendWithBuilder("DepthAdd");
        public string DepthSubtractId => this.Id.AppendWithBuilder("DepthSubtract");
        public string DepthContainerId => this.Id.AppendWithBuilder("DepthContainer");

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {
                var jsRes = new List<JsResource>
                {
                    new JsResource("JsCompositeDateExt", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CompositeDateExt.js", 200,
                        typeof(CompositeDateExtComponent))
                };
                return new ReadOnlyCollection<JsResource>(jsRes);
            }
        }

        public CompositeDateExtComponent()
        {
            this.CustomLabel = new CustomLabelComponent();
            this.ViewModel = new CompositeDateExtViewModel();
            this.Format = DateTimeConstants.EnglishFormat;
            this.OnDateTypeChange = "null";
            this.DisplayTime = false;
            this.AvailableDateTypes = new List<EnumDateTypes>();
            this.DateTypeItems = DateExtSelectionDropDown.GetAllDateExtTypes();
        }

        public override IHtmlContent RenderHtml()
        {
            return new CompositeDateExtHtmlBuilder(this).Build();
        }

        public override string RenderInitScript()
        {
            if (!this.IsVisible) return string.Empty;

            var sb = new StringBuilder();
            var dropDownId = this.DateTypeDropDownId.JQuerySelectorEscape();

            sb.AppendLine("<script type=\"text/javascript\">");
            sb.Append("$(function(){$('#").Append(dropDownId).Append("').compositeDateExt({");
            sb.Append("dateFromContainer:'#").Append(this.DateFromContainerId.JQuerySelectorEscape()).Append("'");
            sb.Append(",dateToContainer:'#").Append(this.DateToContainerId.JQuerySelectorEscape()).Append("'");
            sb.Append(",weekFromContainer:'#").Append(this.WeekFromContainerId.JQuerySelectorEscape()).Append("'");
            sb.Append(",weekToContainer:'#").Append(this.WeekToContainerId.JQuerySelectorEscape()).Append("'");
            sb.Append(",modelContainer:'#").Append(this.ModelContainerId.JQuerySelectorEscape()).Append("'");
            sb.Append(",depthContainer:'#").Append(this.DepthContainerId.JQuerySelectorEscape()).Append("'");
            sb.Append(",onDateTypeChange:").Append(this.OnDateTypeChange);
            sb.Append("});});");
            sb.AppendLine("</script>");

            return sb.ToString();
        }
    }
}
