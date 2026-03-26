namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.Encodings.Web;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class DateTimeHtmlBuilder : HtmlBuilderBase<DateTimeComponent>
    {
        public DateTimeHtmlBuilder(DateTimeComponent component)
        {
            this.Component = component;
        }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible)
            {
                return HtmlString.Empty;
            }

            var tagBuilderMainDiv = new TagBuilder("div");
            if (!string.IsNullOrEmpty(this.Component.CssMainDiv))
            {
                tagBuilderMainDiv.AddCssClass(this.Component.CssMainDiv);
            }
            tagBuilderMainDiv.Attributes["id"] = this.Component.MainDivId;

            var sb = new StringBuilder();

            // Custom label
            if (!string.IsNullOrEmpty(this.Component.CustomLabel.Text))
            {
                sb.Append(this.Component.CustomLabel.ToHtmlString());
            }

            // Date div
            var tagBuilderDateDiv = new TagBuilder("div");
            if (!string.IsNullOrEmpty(this.Component.CssClassDateDiv))
            {
                tagBuilderDateDiv.AddCssClass(this.Component.CssClassDateDiv);
            }

            var dateDivContent = new StringBuilder();

            // Date input
            dateDivContent.Append(CreateDateTag());

            // Hidden format field
            dateDivContent.Append(CreateHiddenFormatTag());

            // Hidden time offset field
            dateDivContent.Append(CreateHiddenTimeOffsetTag());

            // Hidden UTC field
            dateDivContent.Append(CreateHiddenUtcTag());

            // Hidden type field
            dateDivContent.Append(CreateHiddenTypeTag());

            // Calendar icon
            dateDivContent.Append(CreateCalendarIcon());

            // Erase button
            if (this.Component.IsUpdatable && this.Component.DisplayEraseButton)
            {
                dateDivContent.Append(CreateEraseImage());
            }

            // Current date selector
            if (this.Component.IsUpdatable && this.Component.DisplayCurrentDateSelector)
            {
                dateDivContent.Append(CreateCurrentDateSelectorImage());
            }

            // Hour/minute dropdowns or hidden inputs
            if (this.Component.DisplayTime)
            {
                if (this.Component.IsUpdatable)
                {
                    dateDivContent.Append(CreateHourDropDown());
                    dateDivContent.Append(CreateColonLabel());
                    dateDivContent.Append(CreateMinDropDown());
                }
                else
                {
                    dateDivContent.Append(CreateHiddenTimeInputs());
                }
            }

            // Information icon
            if (this.Component.DisplayInformationIcon && !string.IsNullOrEmpty(this.Component.InformationIcon.Text))
            {
                dateDivContent.Append(this.Component.InformationIcon.ToHtmlString());
            }

            // Validation span
            dateDivContent.Append(GetValidationSpan());

            tagBuilderDateDiv.InnerHtml.AppendHtml(dateDivContent.ToString());

            sb.Append(GetTagBuilderHtml(tagBuilderDateDiv));

            tagBuilderMainDiv.InnerHtml.AppendHtml(sb.ToString());

            return tagBuilderMainDiv;
        }

        private string CreateDateTag()
        {
            var tagBuilderDate = new TagBuilder("input");
            tagBuilderDate.Attributes["id"] = this.Component.GetDateTextId;
            var txtDateName = this.Component.GetUpdatableDateTextName;

            if (!string.IsNullOrEmpty(this.Component.Name))
            {
                tagBuilderDate.Attributes["name"] = txtDateName;
            }

            tagBuilderDate.Attributes["type"] = "text";
            tagBuilderDate.Attributes["otherdateid"] = this.Component.AssociatedDateHtmlId;
            tagBuilderDate.AddCssClass("dateHasError");

            if (string.IsNullOrEmpty(this.Component.CssClassDateInput))
            {
                this.Component.CssClassDateInput = "dateTextbox";
            }

            if (!string.IsNullOrEmpty(this.Component.CssClassDateInput))
            {
                tagBuilderDate.AddCssClass(this.Component.CssClassDateInput);
            }

            if (!this.Component.IsUpdatable)
            {
                tagBuilderDate.AddCssClass("readonly");
                tagBuilderDate.Attributes["readonly"] = "readonly";
            }

            var dateString = this.Component.Value.DateText;
            tagBuilderDate.Attributes["value"] = dateString;

            if (this.Component.IsUpdatable)
            {
                foreach (var attr in this.Component.GetUnobtrusiveValidationAttributes())
                {
                    tagBuilderDate.Attributes[attr.Key] = attr.Value?.ToString();
                }
                foreach (var attr in this.Component.ConditionalAnnotations)
                {
                    tagBuilderDate.Attributes[attr.Key] = attr.Value?.ToString();
                }
                if (this.Component.IsMandatory)
                {
                    tagBuilderDate.Attributes["data-val"] = "true";
                    var mandatoryErrorMessage = string.Format(CultureInfo.CurrentCulture, ApplicationStrings.MSG000004, this.Component.ExternalLabelText);
                    if (!string.IsNullOrEmpty(this.Component.MandatoryMessage))
                    {
                        mandatoryErrorMessage = this.Component.MandatoryMessage;
                    }
                    tagBuilderDate.Attributes["data-val-daterequired"] = mandatoryErrorMessage;
                }

                AddValidationAttributesForDate(tagBuilderDate);
            }

            foreach (var attr in this.Component.HtmlAttributes)
            {
                if (!tagBuilderDate.Attributes.ContainsKey(attr.Key))
                {
                    tagBuilderDate.Attributes[attr.Key] = attr.Value?.ToString();
                }
            }

            tagBuilderDate.Attributes["maxlength"] = this.Component.Value.IsModel ? "5" : "10";
            tagBuilderDate.TagRenderMode = TagRenderMode.SelfClosing;

            return GetTagBuilderHtml(tagBuilderDate);
        }

        private void AddValidationAttributesForDate(TagBuilder tagBuilder)
        {
            if (!this.Component.Value.IsModel)
            {
                tagBuilder.Attributes["data-val-dateformat"] = string.Format(
                    CultureInfo.CurrentCulture, ApplicationStrings.ERR_DATE_INVALIDE,
                    this.Component.ExternalLabelText);
            }
        }

        private string CreateHiddenFormatTag()
        {
            var formatName = !string.IsNullOrEmpty(this.Component.Name)
                ? this.Component.Name.AppendWithBuilder(".Format")
                : this.Component.Id.AppendWithBuilder(".Format");

            var format = this.Component.Value.Format == DateTimeConstants.FrenchFormat
                ? DateTimeConstants.FrenchFormat
                : DateTimeConstants.EnglishFormat;

            return CreateHiddenTag(this.Component.Id.AppendWithBuilder("HdnFormat"), formatName, format);
        }

        private string CreateHiddenTimeOffsetTag()
        {
            var timeOffsetName = !string.IsNullOrEmpty(this.Component.Name)
                ? this.Component.Name.AppendWithBuilder(".TimeOffset")
                : this.Component.Id.AppendWithBuilder(".TimeOffset");

            var offsetValue = this.Component.Value.TimeOffset.HasValue
                ? this.Component.Value.TimeOffset.Value.ToString(CultureInfo.InvariantCulture)
                : "1";

            return CreateHiddenTag(this.Component.Id.AppendWithBuilder("HdnTimeOffset"), timeOffsetName, offsetValue);
        }

        private string CreateHiddenUtcTag()
        {
            var utcName = !string.IsNullOrEmpty(this.Component.Name)
                ? this.Component.Name.AppendWithBuilder(".Utc")
                : this.Component.Id.AppendWithBuilder(".Utc");

            var utcValue = this.Component.Value.IsUtcMode ? DateTimeConstants.IsUtc : DateTimeConstants.IsNonUtc;

            return CreateHiddenTag(this.Component.Id.AppendWithBuilder("HdnUtc"), utcName, utcValue);
        }

        private string CreateHiddenTypeTag()
        {
            var typeName = !string.IsNullOrEmpty(this.Component.Name)
                ? this.Component.Name.AppendWithBuilder(".Type")
                : this.Component.Id.AppendWithBuilder(".Type");

            var typeValue = this.Component.Value.IsModel ? DateTimeConstants.ModelFormat : DateTimeConstants.StandardFormat;

            return CreateHiddenTag(this.Component.DateTypeId, typeName, typeValue);
        }

        private string CreateHiddenTag(string id, string name, string value)
        {
            var tag = new TagBuilder("input");
            tag.Attributes["type"] = "hidden";
            tag.Attributes["id"] = id;
            tag.Attributes["name"] = name;
            tag.Attributes["value"] = value ?? string.Empty;
            tag.TagRenderMode = TagRenderMode.SelfClosing;
            return GetTagBuilderHtml(tag);
        }

        private string CreateCalendarIcon()
        {
            var tag = new TagBuilder("span");
            tag.AddCssClass("calendar-icon");
            tag.Attributes["id"] = this.Component.Id.AppendWithBuilder("CalendarIcon");
            return GetTagBuilderHtml(tag);
        }

        private string CreateHourDropDown()
        {
            var tag = new TagBuilder("select");
            tag.Attributes["id"] = this.Component.GetHourDropDownId;
            tag.Attributes["name"] = this.Component.GetUpdatableHourDropDownName;
            tag.AddCssClass("hourDropDown");

            var sb = new StringBuilder();
            var hoursSource = new List<SelectListItem>();

            if (this.Component.Value.Format == DateTimeConstants.FrenchFormat)
            {
                SetHourSourceFr(hoursSource);
            }
            else
            {
                SetHourSourceEng(hoursSource);
            }

            foreach (var item in hoursSource)
            {
                var optionTag = new TagBuilder("option");
                optionTag.Attributes["value"] = item.Value;
                if (item.Value == (this.Component.Value.HourValue ?? "0"))
                {
                    optionTag.Attributes["selected"] = "selected";
                }
                optionTag.InnerHtml.Append(item.Text);
                sb.Append(GetTagBuilderHtml(optionTag));
            }

            tag.InnerHtml.AppendHtml(sb.ToString());
            return GetTagBuilderHtml(tag);
        }

        private string CreateColonLabel()
        {
            var tag = new TagBuilder("span");
            tag.AddCssClass("colon-separator");
            tag.InnerHtml.Append(":");
            return GetTagBuilderHtml(tag);
        }

        private string CreateMinDropDown()
        {
            var tag = new TagBuilder("select");
            tag.Attributes["id"] = this.Component.GetMinuteDropDownId;
            tag.Attributes["name"] = this.Component.GetUpdatableMinDropDownName;
            tag.AddCssClass("minuteDropDown");

            var sb = new StringBuilder();
            for (int minute = 0; minute <= 59; minute++)
            {
                var optionTag = new TagBuilder("option");
                optionTag.Attributes["value"] = minute.ToString(CultureInfo.InvariantCulture);
                if (minute.ToString(CultureInfo.InvariantCulture) == (this.Component.Value.MinuteValue ?? "0"))
                {
                    optionTag.Attributes["selected"] = "selected";
                }
                optionTag.InnerHtml.Append(minute.ToString("00", CultureInfo.InvariantCulture));
                sb.Append(GetTagBuilderHtml(optionTag));
            }

            tag.InnerHtml.AppendHtml(sb.ToString());
            return GetTagBuilderHtml(tag);
        }

        private string CreateHiddenTimeInputs()
        {
            var hourToSet = string.Empty;
            var minuteToSet = string.Empty;
            var dateAndFormat = this.Component.Value;
            if (dateAndFormat != null)
            {
                if (this.Component.DisplayTime)
                {
                    hourToSet = dateAndFormat.HourValue;
                    minuteToSet = dateAndFormat.MinuteValue;
                }
                else
                {
                    if (this.Component.Value.Date.HasValue)
                    {
                        hourToSet = "0";
                        minuteToSet = "0";
                    }
                }
            }

            var hourHtml = CreateHiddenTag(this.Component.HiddenHourId, this.Component.GetUpdatableHourDropDownName, hourToSet);
            var minuteHtml = CreateHiddenTag(this.Component.HiddenMinuteId, this.Component.GetUpdatableMinDropDownName, minuteToSet);

            return hourHtml + minuteHtml;
        }

        private void SetHourSourceEng(List<SelectListItem> hoursSourceItems)
        {
            if (this.Component.IsUpdatable)
            {
                hoursSourceItems.Add(new SelectListItem { Text = "12am", Value = "0" });
                for (var hour = 1; hour <= 23; hour++)
                {
                    var text = GetCurItemEng(hour);
                    hoursSourceItems.Add(new SelectListItem { Text = text, Value = hour.ToString(CultureInfo.InvariantCulture) });
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.Component.Value.HourValue))
                {
                    var hour = Convert.ToInt32(this.Component.Value.HourValue, CultureInfo.InvariantCulture);
                    var text = GetCurItemEng(hour);
                    hoursSourceItems.Add(new SelectListItem { Text = text, Value = this.Component.Value.HourValue });
                }
                else
                {
                    hoursSourceItems.Add(new SelectListItem { Text = "12am", Value = "0" });
                }
            }
        }

        private static string GetCurItemEng(int hour)
        {
            var sb = new StringBuilder();
            sb.Append(hour <= 12 ? hour.ToString("00", CultureInfo.InvariantCulture) : (hour % 12).ToString("00", CultureInfo.InvariantCulture));
            sb.Append(hour <= 11 ? ApplicationStrings.AM : ApplicationStrings.PM);
            return sb.ToString();
        }

        private void SetHourSourceFr(List<SelectListItem> hoursSourceItems)
        {
            if (this.Component.IsUpdatable)
            {
                for (var hour = 0; hour <= 23; hour++)
                {
                    hoursSourceItems.Add(new SelectListItem
                    {
                        Text = hour.ToString("00", CultureInfo.InvariantCulture),
                        Value = hour.ToString(CultureInfo.InvariantCulture)
                    });
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.Component.Value.HourValue))
                {
                    var hour = Convert.ToInt32(this.Component.Value.HourValue, CultureInfo.InvariantCulture);
                    hoursSourceItems.Add(new SelectListItem
                    {
                        Text = hour.ToString("00", CultureInfo.InvariantCulture),
                        Value = this.Component.Value.HourValue
                    });
                }
                else
                {
                    hoursSourceItems.Add(new SelectListItem { Text = "00", Value = "0" });
                }
            }
        }

        private string CreateEraseImage()
        {
            var tag = new TagBuilder("a");
            tag.Attributes["id"] = this.Component.GetEraseButtonId;
            tag.AddCssClass("cleanImage");
            tag.Attributes["href"] = "###";

            var eraseText = this.Component.EraseButtonText;
            if (string.IsNullOrEmpty(eraseText))
            {
                eraseText = string.Format(CultureInfo.CurrentCulture, ApplicationStrings.CleanImageTooltip, this.Component.ExternalLabelText);
            }
            tag.Attributes["title"] = eraseText;

            var imgPath = this.Component.EraseImagePath;
            if (string.IsNullOrEmpty(imgPath))
            {
                imgPath = "/Images/picto-errase.png";
            }

            var imgTag = new TagBuilder("img");
            imgTag.Attributes["src"] = imgPath;
            imgTag.Attributes["alt"] = eraseText;
            imgTag.TagRenderMode = TagRenderMode.SelfClosing;
            tag.InnerHtml.AppendHtml(GetTagBuilderHtml(imgTag));

            return GetTagBuilderHtml(tag);
        }

        private string CreateCurrentDateSelectorImage()
        {
            var tag = new TagBuilder("a");
            tag.Attributes["id"] = this.Component.GetCurrentDateImageId;
            tag.AddCssClass("cleanImage");
            tag.Attributes["href"] = "###";

            var imagePath = this.Component.CurrentDateSelectionImageUrl.IsEmpty()
                ? "/Images/picto-clock.png"
                : this.Component.CurrentDateSelectionImageUrl;
            var imageTitle = this.Component.CurrentDateSelectionTitle.IsEmpty()
                ? string.Format(CultureInfo.CurrentCulture, ApplicationStrings.TIP000023, this.Component.ExternalLabelText)
                : this.Component.CurrentDateSelectionTitle;

            tag.Attributes["title"] = imageTitle;

            var imgTag = new TagBuilder("img");
            imgTag.Attributes["src"] = imagePath;
            imgTag.Attributes["alt"] = imageTitle;
            imgTag.TagRenderMode = TagRenderMode.SelfClosing;
            tag.InnerHtml.AppendHtml(GetTagBuilderHtml(imgTag));

            return GetTagBuilderHtml(tag);
        }

        private string GetValidationSpan()
        {
            var tag = new TagBuilder("span");
            tag.AddCssClass("field-validation-valid");
            tag.Attributes["data-valmsg-for"] = this.Component.GetUpdatableDateTextName;
            tag.Attributes["data-valmsg-replace"] = "true";
            return GetTagBuilderHtml(tag);
        }

        private static string GetTagBuilderHtml(TagBuilder tagBuilder)
        {
            using (var writer = new StringWriter())
            {
                tagBuilder.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}
