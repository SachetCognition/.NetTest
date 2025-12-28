using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent;
using Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Label;
using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
using Equant.SAV2000.ComponentLibrary.MVC.Helpers;
using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;

public class DateTimeComponent : ComponentBase
{
    private DateTimeWithFormat _dateValue;
    private string _onDateChange = "null";

    public CustomLabelComponent CustomLabel { get; set; }
    internal DropDownListComponent DropDownListHour { get; set; }
    internal DropDownListComponent DropDownListMinute { get; set; }
    internal LabelComponent ConsumingAppArea { get; set; }
    public string? ConsumingAppAreaId { get; set; }
    internal LabelComponent LabelColon { get; set; }
    internal HyperLinkComponent EraseImage { get; set; }
    internal HyperLinkComponent CurrentDateImage { get; set; }
    internal LabelComponent LabelDateReadOnly { get; set; }
    public ImageToolTipComponent InformationIcon { get; set; }
    public string? CurrentDateSelectionImageUrl { get; set; }
    public string? CurrentDateSelectionTitle { get; set; }

    public DateTimeComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        _onDateChange = "null";
        CustomLabel = new CustomLabelComponent(HtmlHelper);
        DisplayTime = true;
        DisplayEraseButton = false;
        DisplayInformationIcon = false;
        _dateValue = new DateTimeWithFormat(DateTimeConstants.EnglishFormat, false);
        AssociatedDateHtmlId = string.Empty;
        DisplayCurrentDateSelector = false;

        InformationIcon = new ImageToolTipComponent(HtmlHelper);
        DropDownListHour = new DropDownListComponent(HtmlHelper);
        EraseImage = new HyperLinkComponent(HtmlHelper);
        LabelColon = new LabelComponent(HtmlHelper);
        LabelDateReadOnly = new LabelComponent(HtmlHelper);
        DropDownListMinute = new DropDownListComponent(HtmlHelper);
        ConsumingAppArea = new LabelComponent(HtmlHelper);
        CurrentDateImage = new HyperLinkComponent(HtmlHelper);
        StartFromCurrentDate = false;
    }

    public override ReadOnlyCollection<JsResource> JsResources
    {
        get
        {
            var jsRes = new List<JsResource>
            {
                new JsResource("JsDatePicker", "Equant.SAV2000.ComponentLibrary.Common.Resources.Javascripts.jquery-ui-datepicker.js", 210, typeof(DateTimeComponent)),
                new JsResource("JsDateTimeCommon", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DateTimeCommon.js", 220, typeof(DateTimeComponent)),
                new JsResource("JsDateTime", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DateTime.js", 230, typeof(DateTimeComponent))
            };

            if (DisplayInformationIcon && InformationIcon != null && !string.IsNullOrEmpty(InformationIcon.Text))
                jsRes.AddRange(InformationIcon.JsResources);

            if (DisplayTime && DropDownListHour != null)
                jsRes.AddRange(DropDownListHour.JsResources);

            if (DisplayTime && DropDownListMinute != null)
                jsRes.AddRange(DropDownListMinute.JsResources);

            if (IsUpdatable && DisplayEraseButton && EraseImage != null)
                jsRes.AddRange(EraseImage.JsResources);

            if (IsUpdatable && DisplayCurrentDateSelector && CurrentDateImage != null)
                jsRes.AddRange(CurrentDateImage.JsResources);

            if (ErrorMessage != null)
                jsRes.AddRange(ErrorMessage.JsResources);

            return new ReadOnlyCollection<JsResource>(jsRes);
        }
    }

    public string? CssMainDiv { get; set; }
    public string? CssClassDateInput { get; set; }

    public string OnDateChange
    {
        get => _onDateChange;
        set { if (!string.IsNullOrEmpty(value)) _onDateChange = value; }
    }

    public ErrorComponents? ErrorMessage { get; set; }
    public string? EraseImagePath { get; set; }
    public string? InformationIconPath { get; set; }
    public string? CalendarImagePath { get; set; }
    public string? CssClassLabelDiv { get; set; }
    public string? CssClassDateDiv { get; set; }

    public DateTimeWithFormat Value
    {
        get => _dateValue;
        set { if (value != null) _dateValue = value; }
    }

    public bool DisplayTime { get; set; }
    public string? EraseButtonText { get; set; }
    public bool DisplayEraseButton { get; set; }
    public bool DisplayCurrentDateSelector { get; set; }
    public bool DisplayInformationIcon { get; set; }
    public string? LabelCssReadOnly { get; set; }
    public bool StartFromCurrentDate { get; set; }
    public bool IsUpdatable { get; set; } = true;
    public string? AssociatedDateHtmlId { get; set; }
    public string? AccessHrText { get; set; }
    public string? AccessMinText { get; set; }

    public string GetHourDropDownId => Id + "DropDownHours";
    public string GetDateTextId => Id + "Date";
    public string GetUpdatableDateTextName => Name + ".Date";
    public string DateTypeId => Id + "Type";
    public string GetEraseButtonId => Id + "EraseImage";
    public string GetCurrentDateImageId => Id + "CurrentDateImage";
    public string GetInformationIconName => Name + "InformationIcon";
    public string GetInformationIconId => Id + "InformationIcon";
    public string GetInformationIconToolTipId => Id + "InformationIconToolTip";
    public string GetUpdatableHourDropDownName => Name + ".DropDownHours";
    public string GetReadOnlyHourDropDownName => Name + "DropDownHoursReadonly";
    public string GetMinuteDropDownId => Id + "DropDownMins";
    public string GetUpdatableMinDropDownName => Name + ".DropDownMins";
    public string GetReadOnlyMinDropDownName => Name + "DropDownMinsReadOnly";
    public string MainDivId => Id + "MainDiv";
    public string HiddenHourId => Id + "HourHidden";
    public string HiddenMinuteId => Id + "MinuteHidden";

    public bool IsMandatory { get; set; }
    public Dictionary<string, string>? ConditionalAnnotations { get; set; }
    public string? MandatoryMessage { get; set; }
    public string? ExternalLabelText { get; set; }

    public override IHtmlContent ToHtml()
    {
        return new DateTimeHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        var calendarText = string.Format(CultureInfo.CurrentCulture, "Select date for {0}", ExternalLabelText);
        var calendarImagePath = string.IsNullOrEmpty(CalendarImagePath) ? "/Images/calendar.png" : CalendarImagePath;

        string jsDateFormat = Value.Format == DateTimeConstants.FrenchFormat
            ? DateTimeConstants.JsFrenchFormat
            : DateTimeConstants.JsEnglishFormat;

        var options = JsonConvert.SerializeObject(new
        {
            onDateChange = new JRaw(OnDateChange),
            calendarImagePath,
            calendarImageText = calendarText,
            culture = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString(),
            dateFormat = jsDateFormat.ToLower(CultureInfo.CurrentCulture),
            ddlHourId = GetHourDropDownId,
            ddlMinuteId = GetMinuteDropDownId,
            txtDateId = GetDateTextId,
            displayTime = DisplayTime,
            eraseButtonId = GetEraseButtonId,
            currentDateImageId = GetCurrentDateImageId,
            timeDefaultValue = DateTimeConstants.TimeDefaultValue,
            isUpdatable = IsUpdatable,
            startFromCurrentDate = StartFromCurrentDate,
            associatedDateHtmlId = AssociatedDateHtmlId,
            dateTypeId = DateTypeId,
            mainDivId = MainDivId,
            hiddenHourId = HiddenHourId,
            hiddenMinuteId = HiddenMinuteId,
            timeZoneOffset = DateComponentHelper.ResolveOffset(Value.TimeOffset),
            utcMode = Value.IsUtcMode,
            dateDivId = Id
        });

        var sb = new StringBuilder();
        sb.AppendLine($"$('#{Id}').dateTime({options});");

        if (DisplayInformationIcon && InformationIcon != null && !string.IsNullOrEmpty(InformationIcon.Text))
            sb.AppendLine(InformationIcon.ToInitScript());

        if (DisplayTime && DropDownListHour != null)
            sb.AppendLine(DropDownListHour.ToInitScript());

        if (DisplayTime && DropDownListMinute != null)
            sb.AppendLine(DropDownListMinute.ToInitScript());

        if (IsUpdatable && DisplayEraseButton && EraseImage != null)
            sb.AppendLine(EraseImage.ToInitScript());

        if (IsUpdatable && DisplayCurrentDateSelector && CurrentDateImage != null)
            sb.AppendLine(CurrentDateImage.ToInitScript());

        if (ErrorMessage != null)
            sb.AppendLine(ErrorMessage.ToInitScript());

        return sb.ToString();
    }
}
