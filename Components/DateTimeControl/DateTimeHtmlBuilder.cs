using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;
using Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Label;
using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
using Equant.SAV2000.ComponentLibrary.MVC.Helpers;
using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;

public class DateTimeHtmlBuilder : HtmlBuilderBase<DateTimeComponent>
{
    public DateTimeHtmlBuilder(DateTimeComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
            return HtmlString.Empty;

        var tagBuilderMainDiv = new TagBuilder("div");
        if (!string.IsNullOrEmpty(Component.CssMainDiv))
            tagBuilderMainDiv.AddCssClass(Component.CssMainDiv);

        tagBuilderMainDiv.MergeAttribute("id", Component.MainDivId);
        var sbTagMainDivInnerHtml = new StringBuilder();

        if (!string.IsNullOrEmpty(Component.CustomLabel.Text))
        {
            Component.ExternalLabelText = Component.CustomLabel.Text;
            Component.CustomLabel.AssociatedControlId = Component.GetDateTextId;
            Component.CustomLabel.AccessText = "Date";

            var tagBuilderLabelDiv = new TagBuilder("div");
            tagBuilderLabelDiv.InnerHtml.AppendHtml(Component.CustomLabel.ToHtml());
            if (!string.IsNullOrEmpty(Component.CssClassLabelDiv))
                tagBuilderLabelDiv.AddCssClass(Component.CssClassLabelDiv);

            sbTagMainDivInnerHtml.Append(RenderTagBuilder(tagBuilderLabelDiv));
        }

        var tagBuilderDateDiv = new TagBuilder("div");
        tagBuilderDateDiv.MergeAttribute("id", Component.Id);
        if (!string.IsNullOrEmpty(Component.CssClassDateDiv))
            tagBuilderDateDiv.AddCssClass(Component.CssClassDateDiv);

        var sbTagDateDivInnerHtml = new StringBuilder();
        sbTagDateDivInnerHtml.Append(CreateDateTag());
        sbTagDateDivInnerHtml.Append(CreateHiddenFormatTag());
        sbTagDateDivInnerHtml.Append(CreateHiddenTypeTag());
        sbTagDateDivInnerHtml.Append(CreateHiddenTimeOffsetTag());
        sbTagDateDivInnerHtml.Append(CreateHiddenUtcTag());

        if (Component.DisplayTime)
        {
            SetHourMinDropDownLists();
            CreateColonLabel();
            sbTagDateDivInnerHtml.Append(RenderHtmlContent(Component.DropDownListHour.ToHtml()));
            sbTagDateDivInnerHtml.Append(RenderHtmlContent(Component.LabelColon.ToHtml()));
            sbTagDateDivInnerHtml.Append(RenderHtmlContent(Component.DropDownListMinute.ToHtml()));
        }

        if (Component.IsUpdatable)
        {
            if (Component.DisplayCurrentDateSelector)
                sbTagDateDivInnerHtml.Append(CreateCurrentDateSelectorImage());

            if (Component.DisplayEraseButton)
                sbTagDateDivInnerHtml.Append(CreateEraseImage());
        }

        if (Component.DisplayInformationIcon)
        {
            Component.InformationIcon.Name = Component.GetInformationIconName;
            Component.InformationIcon.Id = Component.GetInformationIconId;
            if (string.IsNullOrEmpty(Component.InformationIconPath))
                Component.InformationIconPath = "/Images/picto-information.png";

            Component.InformationIcon.ImageUrl = Component.InformationIconPath;
            Component.InformationIcon.ToolTipId = Component.GetInformationIconToolTipId;
            sbTagDateDivInnerHtml.Append(RenderHtmlContent(Component.InformationIcon.ToHtml()));
        }

        if (Component.IsUpdatable && !string.IsNullOrEmpty(Component.ConsumingAppAreaId))
            sbTagDateDivInnerHtml.Append(GetConsumingAppAreaHtml());

        if (Component.IsUpdatable && Component.ValidationString != null)
        {
            Component.ValidationString = new HtmlString(GetValidationSpan(Component.GetUpdatableDateTextName));
            Component.ErrorMessage = ErrorHelper.CreateErrorComponent(Component);
            sbTagDateDivInnerHtml.Append(RenderHtmlContent(Component.ErrorMessage.ToHtml()));
        }

        if (Component.IsUpdatable && !Component.DisplayTime)
            sbTagDateDivInnerHtml.Append(CreateHiddenTimeInputs());
        else if (!Component.IsUpdatable && Component.DisplayTime)
            sbTagDateDivInnerHtml.Append(CreateHiddenTimeInputs());

        tagBuilderDateDiv.InnerHtml.AppendHtml(sbTagDateDivInnerHtml.ToString());
        sbTagMainDivInnerHtml.Append(RenderTagBuilder(tagBuilderDateDiv));
        tagBuilderMainDiv.InnerHtml.AppendHtml(sbTagMainDivInnerHtml.ToString());

        return tagBuilderMainDiv;
    }

    private static string RenderTagBuilder(TagBuilder tagBuilder)
    {
        using var writer = new StringWriter();
        tagBuilder.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }

    private static string RenderHtmlContent(IHtmlContent content)
    {
        using var writer = new StringWriter();
        content.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }

        /// <summary>
        /// The get validation span for Date.
        /// </summary>
        /// <param name="forName">
        /// The for name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetValidationSpan(string forName)
        {
            if (MvcHtmlString.IsNullOrEmpty(this.Component.ValidationString))
            {
                return string.Empty;
            }

            var name = this.Component.Name;
            var htmlString = this.Component.ValidationString.ToString();
            return htmlString.Replace("data-valmsg-for=\"" + name + "\"", "data-valmsg-for=\"" + forName + "\"");
        }

        /// <summary>
        /// The create date tag.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateDateTag()
        {
            var tagBuilderDate = new TagBuilder("input");
            tagBuilderDate.MergeAttribute("id", this.Component.GetDateTextId);
            var txtDateName = this.Component.GetUpdatableDateTextName;

            if (!string.IsNullOrEmpty(this.Component.Name))
            {
                tagBuilderDate.MergeAttribute("name", txtDateName);
            }

            tagBuilderDate.MergeAttribute("type", "text");
            tagBuilderDate.MergeAttribute("otherdateid", this.Component.AssociatedDateHtmlId);
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
                tagBuilderDate.MergeAttribute("readonly", "readonly");
            }

            var dateString = this.Component.Value.DateText;
            tagBuilderDate.MergeAttribute("value", dateString);

            if (this.Component.IsUpdatable)
            {
                tagBuilderDate.MergeAttributes(this.Component.GetUnobtrusiveValidationAttributes());
                this.AddValidationAttributesForDate();
                tagBuilderDate.MergeAttributes(this.Component.ConditionalAnnotations);
                if (this.Component.IsMandatory)
                {
                    tagBuilderDate.MergeAttribute("data-val", "true");
                    var mandatoryErrorMessage = string.Format(CultureInfo.CurrentCulture, ApplicationStrings.MSG000004, this.Component.ExternalLabelText);
                    if (!string.IsNullOrEmpty(this.Component.MandatoryMessage))
                    {
                        mandatoryErrorMessage = this.Component.MandatoryMessage;
                    }

                    tagBuilderDate.MergeAttribute("data-val-daterequired", mandatoryErrorMessage);
                }
            }

            tagBuilderDate.MergeAttributes(this.Component.HtmlAttributes);
            tagBuilderDate.MergeAttribute("maxlength", this.Component.Value.IsModel ? "5" : "10");
            return tagBuilderDate.ToString(TagRenderMode.StartTag);
        }

        /// <summary>
        /// This adds validation attributes.
        /// </summary>
        private void AddValidationAttributesForDate()
        {
            this.Component.EnableValidationAttribute();
            //add this component's Id to call function on datetime object on client side
            this.Component.AddValidationAttribute("dateObjectId", this.Component.Id);
            this.Component.AddValidationAttribute("offset", JsonConvert.SerializeObject(this.Component.Value.TimeOffset));
            this.Component.AddValidationAttribute("utcmode", JsonConvert.SerializeObject(this.Component.Value.IsUtcMode));
            //add validation attribute for date format
            const string RuleName = "dateformat";
            this.Component.AddValidationAttribute(RuleName, "The date format is incorrect");
            var format = this.Component.Value.Format == DateTimeConstants.EnglishFormat ? DateTimeConstants.JsEnglishFormat :
                DateTimeConstants.JsFrenchFormat;
            var strType = this.Component.Value.IsModel ? "model" : "standard";
            this.Component.AddValidationAttributeProperty(RuleName, "type", strType);
            this.Component.AddValidationAttributeProperty(RuleName, "format", format);
            this.Component.AddValidationAttributeProperty(RuleName, "defaultValue", DateTimeConstants.TimeDefaultValue);
            this.Component.AddValidationAttributeProperty(RuleName, "hourDropDown", this.Component.GetHourDropDownId);
            this.Component.AddValidationAttributeProperty(RuleName, "minuteDropDown", this.Component.GetMinuteDropDownId);
            this.Component.AddValidationAttributeProperty(RuleName, "msgIncorrectTime",
                string.Format(CultureInfo.CurrentCulture, ApplicationStrings.TimeMandatory, this.Component.ExternalLabelText));
            this.Component.AddValidationAttributeProperty(RuleName, "msgIncorrectDate",
                string.Format(CultureInfo.CurrentCulture, ApplicationStrings.ERR_DATE_INVALIDE, this.Component.ExternalLabelText));

            //add attribute if time is displayed or not
            var displayTime = this.Component.DisplayTime ? "true" : "false";
            this.Component.AddValidationAttributeProperty(RuleName, "displayTime", displayTime);
        }

        /// <summary>
        /// The create date time tag.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateHiddenFormatTag()
        {
            return this.CreateHiddenTag(this.Component.Id.AppendWithBuilder("Format"),
                this.Component.Name.AppendWithBuilder(".Format"), this.Component.Value.Format);
        }

        /// <summary>
        /// The create hidden time offset tag.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateHiddenTimeOffsetTag()
        {
            return this.CreateHiddenTag(this.Component.Id.AppendWithBuilder("TimeOffset"),
                this.Component.Name.AppendWithBuilder(".TimeOffset"), DateComponentHelper.ResolveOffset(this.Component.Value.TimeOffset).
                ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// The create hidden UTC tag.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateHiddenUtcTag()
        {
            return this.CreateHiddenTag(this.Component.Id.AppendWithBuilder("Utc"),
                this.Component.Name.AppendWithBuilder(".Utc"), this.Component.Value.IsUtcMode ? DateTimeConstants.IsUtc : DateTimeConstants.IsNonUtc);
        }

        /// <summary>
        /// The create hidden type tag.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateHiddenTypeTag()
        {
            var strType = this.Component.Value.IsModel ? "model" : "standard";
            return this.CreateHiddenTag(this.Component.DateTypeId, this.Component.Name.AppendWithBuilder(".Type"), strType);
        }

        /// <summary>
        /// The create date time tag.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateHiddenTag(string id, string name, string value)
        {
            var tagBuilderHidden = new TagBuilder("input");
            tagBuilderHidden.MergeAttribute("id", id);

            if (!string.IsNullOrEmpty(this.Component.Name))
            {
                tagBuilderHidden.MergeAttribute("name", name);
            }

            tagBuilderHidden.MergeAttribute("type", "hidden");
            tagBuilderHidden.MergeAttribute("value", value);
            return tagBuilderHidden.ToString(TagRenderMode.StartTag);
        }

        /// <summary>
        /// This sets the hour and minutes drop down lists.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void SetHourMinDropDownLists()
        {
            var dateAndFormat = this.Component.Value;
            var hourToSelect = string.Empty;
            var minuteToSelect = string.Empty;
            if (dateAndFormat != null)
            {
                hourToSelect = dateAndFormat.HourValue;
                minuteToSelect = dateAndFormat.MinuteValue;
            }

            this.CreateHourDropDown(hourToSelect);
            this.CreateMinDropDown(minuteToSelect);
        }

        /// <summary>
        /// This creates colon label.
        /// </summary>
        private void CreateColonLabel()
        {
            new LabelBuilder(this.Component.LabelColon, this.Component.LabelColon.ModelMetadata).CssClassLabel("seperator").Text(":")
            .HtmlAttributes(new { Id = this.Component.Id.AppendWithBuilder("Colon") });
        }

        /// <summary>
        /// This creates minutes drop down.
        /// </summary>
        private void CreateMinDropDown(string minuteToSelect)
        {
            SelectListItem[] minsSourceItems;
            if (this.Component.IsUpdatable)
            {
                minsSourceItems = new SelectListItem[60];
                for (int min = 0; min <= 59; min++)
                {
                    var sbCurItemText = new StringBuilder();
                    sbCurItemText.Append(min.ToString("00", CultureInfo.InvariantCulture));
                    minsSourceItems[min] = new SelectListItem { Text = sbCurItemText.ToString(), Value = min.ToString(CultureInfo.InvariantCulture) };
                }
            }
            else
            {
                minsSourceItems = new SelectListItem[1];
                string selectedValue;
                string selectedText;
                if (!string.IsNullOrEmpty(this.Component.Value.MinuteValue))
                {
                    selectedValue = this.Component.Value.MinuteValue;
                    //the selected value can be converted because in the datetimewithformat class it is checked before it is assigned.
                    selectedText = Convert.ToInt32(selectedValue, CultureInfo.InvariantCulture).ToString("00", CultureInfo.InvariantCulture);
                }
                else
                {
                    selectedValue = "0";
                    selectedText = 0.ToString("00", CultureInfo.InvariantCulture);
                }

                minsSourceItems[0] = new SelectListItem { Text = selectedText, Value = selectedValue };
            }

            var minsSelectList = new SelectList(minsSourceItems, "Value", "Text", minuteToSelect);

            var ddlMinId = this.Component.GetMinuteDropDownId;
            var ddlMinName = this.Component.GetUpdatableMinDropDownName;

            var isDisabled = string.IsNullOrEmpty(this.Component.Value.DateText) && string.IsNullOrEmpty(this.Component.Value.HourValue)
                && string.IsNullOrEmpty(this.Component.Value.MinuteValue);

            if (isDisabled || !this.Component.IsUpdatable)
            {
                this.Component.DropDownListMinute.HtmlAttributes.Add("disabled", "disabled");
            }

            if (!this.Component.IsUpdatable)
            {
                ddlMinName = this.Component.GetReadOnlyMinDropDownName;
            }

            this.Component.DropDownListMinute.IsDivNeeded = false;
            //Adding hidden label span access text.
            if (string.IsNullOrEmpty(this.Component.AccessMinText))
            {
                this.Component.AccessMinText = string.Format(CultureInfo.CurrentCulture,
                    ApplicationStrings.LBL000024, this.Component.ExternalLabelText);
            }

            new DropDownListBuilder(this.Component.DropDownListMinute, this.Component.DropDownListMinute.ModelMetadata)
                .Name(ddlMinName)
                .Id(ddlMinId)
                .DataBind(minsSelectList)
                .CustomLabel(m => m.Text(this.Component.AccessMinText)
                .AssociatedControlId(ddlMinId)
                .HtmlAttributes(new { Id = ddlMinId + "lbl" })
                .IsOnlyForAccess(true));
        }

        /// <summary>
        /// The create hour drop down english.
        /// </summary>
        private void CreateHourDropDown(string hourToSelect)
        {
            var hoursSourceItems = this.Component.IsUpdatable ? new SelectListItem[24] : new SelectListItem[1];

            if (this.Component.Value.Format == DateTimeConstants.EnglishFormat)
            {
                this.SetHourSourceEng(hoursSourceItems);
            }
            else
            {
                this.SetHourSourceFr(hoursSourceItems);
            }

            var hoursSelectList = new SelectList(hoursSourceItems, "Value", "Text", hourToSelect);
            var ddlHourId = this.Component.GetHourDropDownId;
            var ddlHourName = this.Component.GetUpdatableHourDropDownName;
            var isDisabled = string.IsNullOrEmpty(this.Component.Value.DateText) && string.IsNullOrEmpty(this.Component.Value.HourValue)
                  && string.IsNullOrEmpty(this.Component.Value.MinuteValue);
            if (isDisabled || !this.Component.IsUpdatable)
            {
                this.Component.DropDownListHour.HtmlAttributes.Add("disabled", "disabled");
            }

            if (!this.Component.IsUpdatable)
            {
                ddlHourName = this.Component.GetReadOnlyHourDropDownName;
            }

            this.Component.DropDownListHour.IsDivNeeded = false;

            if (string.IsNullOrEmpty(this.Component.AccessHrText))
            {
                this.Component.AccessHrText = string.Format(CultureInfo.CurrentCulture, ApplicationStrings.LBL000023,
                    this.Component.ExternalLabelText);
            }

            new DropDownListBuilder(this.Component.DropDownListHour, this.Component.DropDownListHour.ModelMetadata)
                .Name(ddlHourName)
                .Id(ddlHourId)
                .DataBind(hoursSelectList)
                .CustomLabel(m => m.Text(this.Component.AccessHrText)
                .AssociatedControlId(ddlHourId)
                .HtmlAttributes(new { Id = ddlHourId + "lbl" })
                .IsOnlyForAccess(true));
        }

        /// <summary>
        /// The get consuming app area html.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetConsumingAppAreaHtml()
        {
            if (!string.IsNullOrEmpty(this.Component.ConsumingAppAreaId))
            {
                //app Area is to be hidden initially
                new LabelBuilder(this.Component.ConsumingAppArea, this.Component.ConsumingAppArea.ModelMetadata).HtmlAttributes(new { Id = this.Component.ConsumingAppAreaId })
                    .CssClassLabel("displaynone");
                return this.Component.ConsumingAppArea.ToHtmlString();
            }

            return string.Empty;
        }

        /// <summary>
        /// This creates hidden time input for readonly mode.
        /// </summary>
        /// <returns>
        /// The hidden time input <see cref="string"/>.
        /// </returns>
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
                    else
                    {
                        hourToSet = string.Empty;
                        minuteToSet = string.Empty;
                    }
                }
            }

            var hdnHourId = this.Component.HiddenHourId;
            var hdnHourName = this.Component.GetUpdatableHourDropDownName;
            var hourHtml = this.CreateHiddenTag(hdnHourId, hdnHourName, hourToSet);

            var hdnMinuteId = this.Component.HiddenMinuteId;
            var hdnMinuteName = this.Component.GetUpdatableMinDropDownName;
            var minuteHtml = this.CreateHiddenTag(hdnMinuteId, hdnMinuteName, minuteToSet);

            return hourHtml.AppendWithBuilder(minuteHtml);
        }

        /// <summary>
        /// This sets the hour source in English.
        /// </summary>
        /// <param name="hoursSourceItems">
        /// The hours source items.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
            MessageId = "System.Web.Mvc.SelectListItem.set_Text(System.String)",
            Justification = "TETHYS: The string localization for values of dropdown is managed through code in this case.")]
        private void SetHourSourceEng(SelectListItem[] hoursSourceItems)
        {
            if (hoursSourceItems == null)
            {
                throw new ArgumentNullException("hoursSourceItems");
            }

            const string CurItemValue = "12am";
            if (this.Component.IsUpdatable)
            {
                hoursSourceItems[0] = new SelectListItem { Text = CurItemValue, Value = 0.ToString(CultureInfo.InvariantCulture) };
                for (var hour = 1; hour <= 23; hour++)
                {
                    var sbCurItemText = GetCurItemEng(hour);
                    hoursSourceItems[hour] = new SelectListItem { Text = sbCurItemText.ToString(), Value = hour.ToString(CultureInfo.InvariantCulture) };
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.Component.Value.HourValue))
                {
                    //this is safe because inside datetimewithformat class this value is parsed into int if not null or empty.
                    var hour = Convert.ToInt32(this.Component.Value.HourValue, CultureInfo.InvariantCulture);
                    var sbCurItemText = GetCurItemEng(hour);
                    hoursSourceItems[0] = new SelectListItem { Text = sbCurItemText.ToString(), Value = this.Component.Value.HourValue };
                }
                else
                {
                    hoursSourceItems[0] = new SelectListItem { Text = CurItemValue, Value = "0" };
                }
            }
        }


        /// <summary>
        /// This sets the hour text.
        /// </summary>
        /// <param name="hour">
        /// The hour's numerical value.
        /// </param>
        private static StringBuilder GetCurItemEng(int hour)
        {
            var sbCurItemText = new StringBuilder();
            sbCurItemText.Append(hour <= 12 ? hour.ToString("00", CultureInfo.InvariantCulture) : (hour % 12).ToString("00", CultureInfo.InvariantCulture));
            sbCurItemText.Append(hour <= 11 ? ApplicationStrings.AM : ApplicationStrings.PM);
            return sbCurItemText;
        }

        /// <summary>
        /// This sets the hour source in French.
        /// </summary>
        /// <param name="hoursSourceItems">
        /// The hours source items.
        /// </param>
        private void SetHourSourceFr(SelectListItem[] hoursSourceItems)
        {
            if (hoursSourceItems == null)
            {
                throw new ArgumentNullException("hoursSourceItems");
            }
            if (this.Component.IsUpdatable)
            {
                for (var hour = 0; hour <= 23; hour++)
                {
                    var sbCurItemText = new StringBuilder();
                    sbCurItemText.Append(hour.ToString("00", CultureInfo.InvariantCulture));
                    hoursSourceItems[hour] = new SelectListItem { Text = sbCurItemText.ToString(), Value = hour.ToString(CultureInfo.InvariantCulture) };
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.Component.Value.HourValue))
                {
                    //this is safe because inside datetimewithformat class this value is parsed into int if not null or empty.
                    var hour = Convert.ToInt32(this.Component.Value.HourValue, CultureInfo.InvariantCulture);
                    var sbCurItemText = new StringBuilder();
                    sbCurItemText.Append(hour.ToString("00", CultureInfo.InvariantCulture));
                    hoursSourceItems[0] = new SelectListItem { Text = sbCurItemText.ToString(), Value = this.Component.Value.HourValue };
                }
                else
                {
                    var selectedText = 0.ToString("00", CultureInfo.InvariantCulture);
                    hoursSourceItems[0] = new SelectListItem { Text = selectedText, Value = "0" };
                }
            }
        }

        /// <summary>
        /// This create current date image.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateCurrentDateSelectorImage()
        {
            string imagePath = this.Component.CurrentDateSelectionImageUrl.IsEmpty() ? "/Images/picto-clock.png" : this.Component.CurrentDateSelectionImageUrl;
            string imageTitle = this.Component.CurrentDateSelectionTitle.IsEmpty() ?
                string.Format(CultureInfo.CurrentCulture, ApplicationStrings.TIP000023, this.Component.ExternalLabelText)
                : this.Component.CurrentDateSelectionTitle;

            new HyperLinkBuilder(this.Component.CurrentDateImage, this.Component.CurrentDateImage.ModelMetadata).Id(this.Component.GetCurrentDateImageId)
                .Title(imageTitle).Css("cleanImage").ActionUrl("###")
                .ImageUrl(imagePath);
            return this.Component.CurrentDateImage.ToHtmlString();
        }

        /// <summary>
        /// The create erase image.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateEraseImage()
        {
            if (this.Component.EraseImagePath.IsEmpty())
            {
                this.Component.EraseImagePath = "/Images/picto-errase.png";
            }

            if (string.IsNullOrEmpty(this.Component.EraseButtonText))
            {
                this.Component.EraseButtonText = string.Format(CultureInfo.CurrentCulture, ApplicationStrings.CleanImageTooltip,
                    this.Component.ExternalLabelText);
            }

            new HyperLinkBuilder(this.Component.EraseImage, this.Component.EraseImage.ModelMetadata).Id(this.Component.GetEraseButtonId)
                .Title(this.Component.EraseButtonText).Css("cleanImage").ActionUrl("###")
                .ImageUrl(this.Component.EraseImagePath);
            return RenderHtmlContent(Component.EraseImage.ToHtml());
    }
}
