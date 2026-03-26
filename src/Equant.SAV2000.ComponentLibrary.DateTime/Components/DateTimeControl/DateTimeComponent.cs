namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Text;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Label;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
    using Microsoft.AspNetCore.Html;

    public class DateTimeComponent : ComponentBase
    {
        private DateTimeWithFormat dateValue;
        private string onDateChange;

        public CustomLabelComponent CustomLabel { get; set; }
        internal DropDownListComponent DropDownListHour { get; set; }
        internal DropDownListComponent DropDownListMinute { get; set; }
        public ImageToolTipComponent InformationIcon { get; set; }
        internal HyperLinkComponent EraseImage { get; set; }
        internal LabelComponent LabelColon { get; set; }
        internal LabelComponent LabelDateReadOnly { get; set; }
        internal LabelComponent ConsumingAppArea { get; set; }
        internal HyperLinkComponent CurrentDateImage { get; set; }
        public ErrorComponents ErrorMessage { get; set; }

        public bool DisplayTime { get; set; }
        public bool DisplayEraseButton { get; set; }
        public bool DisplayInformationIcon { get; set; }
        public bool DisplayCurrentDateSelector { get; set; }
        public bool StartFromCurrentDate { get; set; }
        public string AssociatedDateHtmlId { get; set; }
        public string CssMainDiv { get; set; }
        public string CssClassDateDiv { get; set; }
        public string CssClassDateInput { get; set; }
        public string EraseImagePath { get; set; }
        public string EraseButtonText { get; set; }
        public string CurrentDateSelectionImageUrl { get; set; }
        public string CurrentDateSelectionTitle { get; set; }
        public string ConsumingAppAreaId { get; set; }

        public string OnDateChange
        {
            get { return this.onDateChange; }
            set { this.onDateChange = value ?? "null"; }
        }

        public DateTimeWithFormat Value
        {
            get { return this.dateValue; }
            set
            {
                if (value != null)
                {
                    this.dateValue = value;
                }
            }
        }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {
                var jsRes = new List<JsResource>
                {
                    new JsResource("JsDateTime", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DateTime.js", 200,
                        typeof(DateTimeComponent)),
                    new JsResource("JsDatePicker", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.jquery-ui-datepicker.js", 100,
                        typeof(DateTimeComponent))
                };
                return new ReadOnlyCollection<JsResource>(jsRes);
            }
        }

        // Computed ID properties
        public string GetHourDropDownId => this.Id.AppendWithBuilder("DdlHour");
        public string GetDateTextId => this.Id.AppendWithBuilder("TxtDate");
        public string GetUpdatableDateTextName => !string.IsNullOrEmpty(this.Name) ? this.Name.AppendWithBuilder(".Date") : this.Id.AppendWithBuilder(".Date");
        public string DateTypeId => this.Id.AppendWithBuilder("HdnType");
        public string GetEraseButtonId => this.Id.AppendWithBuilder("LnkErase");
        public string GetCurrentDateImageId => this.Id.AppendWithBuilder("LnkCurrentDate");
        public string GetInformationIconName => this.Id.AppendWithBuilder("Name", "InformationIcon");
        public string GetInformationIconId => this.Id.AppendWithBuilder("InformationIcon");
        public string GetInformationIconToolTipId => this.Id.AppendWithBuilder("InformationIconToolTip");
        public string GetUpdatableHourDropDownName => !string.IsNullOrEmpty(this.Name) ? this.Name.AppendWithBuilder(".Hour") : this.Id.AppendWithBuilder(".Hour");
        public string GetReadOnlyHourDropDownName => this.Id.AppendWithBuilder("DdlHourRO");
        public string GetMinuteDropDownId => this.Id.AppendWithBuilder("DdlMinute");
        public string GetUpdatableMinDropDownName => !string.IsNullOrEmpty(this.Name) ? this.Name.AppendWithBuilder(".Minute") : this.Id.AppendWithBuilder(".Minute");
        public string GetReadOnlyMinDropDownName => this.Id.AppendWithBuilder("DdlMinuteRO");
        public string MainDivId => this.Id.AppendWithBuilder("MainDiv");
        public string HiddenHourId => this.Id.AppendWithBuilder("HdnHour");
        public string HiddenMinuteId => this.Id.AppendWithBuilder("HdnMinute");

        public DateTimeComponent()
        {
            this.onDateChange = "null";
            this.CustomLabel = new CustomLabelComponent();
            this.DisplayTime = true;
            this.DisplayEraseButton = false;
            this.DisplayInformationIcon = false;
            this.dateValue = new DateTimeWithFormat(DateTimeConstants.EnglishFormat, false);
            this.AssociatedDateHtmlId = string.Empty;
            this.DisplayCurrentDateSelector = false;
            this.InformationIcon = new ImageToolTipComponent();
            this.DropDownListHour = new DropDownListComponent();
            this.EraseImage = new HyperLinkComponent();
            this.LabelColon = new LabelComponent();
            this.LabelDateReadOnly = new LabelComponent();
            this.DropDownListMinute = new DropDownListComponent();
            this.ConsumingAppArea = new LabelComponent();
            this.CurrentDateImage = new HyperLinkComponent();
            this.StartFromCurrentDate = false;
        }

        public override IHtmlContent RenderHtml()
        {
            return new DateTimeHtmlBuilder(this).Build();
        }

        public override string RenderInitScript()
        {
            if (!this.IsVisible) return string.Empty;

            var sb = new StringBuilder();
            var dateId = this.GetDateTextId.JQuerySelectorEscape();
            var mainDivId = this.MainDivId.JQuerySelectorEscape();

            sb.AppendLine("<script type=\"text/javascript\">");
            sb.Append("$(function(){$('#").Append(dateId).Append("').dateTime({");

            var jsFormat = this.Value.Format == DateTimeConstants.FrenchFormat
                ? DateTimeConstants.JsFrenchFormat
                : DateTimeConstants.JsEnglishFormat;

            sb.Append("dateFormat:'").Append(jsFormat).Append("'");
            sb.Append(",displayTime:").Append(this.DisplayTime ? "true" : "false");
            sb.Append(",isUpdatable:").Append(this.IsUpdatable ? "true" : "false");
            sb.Append(",onDateChange:").Append(this.OnDateChange);

            if (!string.IsNullOrEmpty(this.AssociatedDateHtmlId))
            {
                sb.Append(",associatedDateId:'").Append(this.AssociatedDateHtmlId.JQuerySelectorEscape()).Append("'");
            }

            if (this.DisplayEraseButton)
            {
                sb.Append(",eraseButtonId:'").Append(this.GetEraseButtonId.JQuerySelectorEscape()).Append("'");
            }

            if (this.DisplayCurrentDateSelector)
            {
                sb.Append(",currentDateImageId:'").Append(this.GetCurrentDateImageId.JQuerySelectorEscape()).Append("'");
            }

            if (this.Value.IsUtcMode)
            {
                sb.Append(",utcMode:true");
            }

            if (this.Value.TimeOffset.HasValue && this.Value.TimeOffset.Value != 1)
            {
                sb.Append(",timeOffset:").Append(this.Value.TimeOffset.Value.ToString(CultureInfo.InvariantCulture));
            }

            sb.Append("});});");
            sb.AppendLine("</script>");

            return sb.ToString();
        }
    }
}
