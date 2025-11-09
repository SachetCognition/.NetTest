// -------------------------------------------------------------------------------------------------
// <copyright file="DateTimeHtmlBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 07/04/2014
//    Author:  Sharma Siddharth (54626) 
//    Description: The Html Builder class for the DateTime component
// </summary>
// -------------------------------------------------------------------------------------------------
namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Label;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

    using Newtonsoft.Json;

    /// <summary>
    /// The Html Builder class for the DateTime component
    /// </summary>
    public class DateTimeHtmlBuilder : HtmlBuilderBase<DateTimeComponent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeHtmlBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        public DateTimeHtmlBuilder(DateTimeComponent component)
        {
            this.Component = component;
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void Build(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentException("The parameter writer cannot be null");
            }

            if (!this.Component.IsVisible)
            {
                return;
            }

            var tagBuilderMainDiv = new TagBuilder("div");

            if (!string.IsNullOrEmpty(this.Component.CssMainDiv))
            {
                tagBuilderMainDiv.AddCssClass(this.Component.CssMainDiv);
            }

            tagBuilderMainDiv.MergeAttribute("id", this.Component.MainDivId);
            var sbTagMainDivInnerHtml = new StringBuilder();
            if (!string.IsNullOrEmpty(this.Component.CustomLabel.Text))
            {
                this.Component.ExternalLabelText = this.Component.CustomLabel.Text;
                //set associated control id for label to date textbox
                this.Component.CustomLabel.AssociatedControlId = this.Component.GetDateTextId;
                //GKG:Fixed for remark 218.Removed the ExternalLabelText which taken as a parameter in string.format method. 
                this.Component.CustomLabel.AccessText = string.Format(CultureInfo.CurrentCulture, ApplicationStrings.LBL000022);

                var tagBuilderLabelDiv = new TagBuilder("div");
            tagBuilderLabelDiv.InnerHtml.AppendHtml(this.Component.CustomLabel.ToHtmlString());
                if (!string.IsNullOrEmpty(this.Component.CssClassLabelDiv))
                {
                    tagBuilderLabelDiv.AddCssClass(this.Component.CssClassLabelDiv);
                }

                sbTagMainDivInnerHtml.Append(tagBuilderLabelDiv);
            }

            var tagBuilderDateDiv = new TagBuilder("div");
            tagBuilderDateDiv.MergeAttribute("id", this.Component.Id);
            if (!string.IsNullOrEmpty(this.Component.CssClassDateDiv))
            {
                tagBuilderDateDiv.AddCssClass(this.Component.CssClassDateDiv);
            }

            //GKG:Fixed Remark 157.Remove only fieldset & legend tag . 
            var sbTagDateDivInnerHtml = new StringBuilder();

            sbTagDateDivInnerHtml.Append(this.CreateDateTag());
            sbTagDateDivInnerHtml.Append(this.CreateHiddenFormatTag());
            sbTagDateDivInnerHtml.Append(this.CreateHiddenTypeTag());
            sbTagDateDivInnerHtml.Append(this.CreateHiddenTimeOffsetTag());
            sbTagDateDivInnerHtml.Append(this.CreateHiddenUtcTag());

            if (this.Component.DisplayTime)
            {
                //set dropdown components for hour and minutes
                this.SetHourMinDropDownLists();
                //set separator label
                this.CreateColonLabel();
                sbTagDateDivInnerHtml.Append(this.Component.DropDownListHour.ToHtmlString());
                sbTagDateDivInnerHtml.Append(this.Component.LabelColon.ToHtmlString());
                sbTagDateDivInnerHtml.Append(this.Component.DropDownListMinute.ToHtmlString());
            }

            if (this.Component.IsUpdatable)
            {
                //add current date selector
                if (this.Component.DisplayCurrentDateSelector)
                {
                    sbTagDateDivInnerHtml.Append(this.CreateCurrentDateSelectorImage());
                }

                //add erase button
                if (this.Component.DisplayEraseButton)
                {
                    sbTagDateDivInnerHtml.Append(this.CreateEraseImage());
                }
            }

            //add information icon
            if (this.Component.DisplayInformationIcon)
            {
                this.Component.InformationIcon.Name = this.Component.GetInformationIconName;
                this.Component.InformationIcon.Id = this.Component.GetInformationIconId;
                if (this.Component.InformationIconPath.IsEmpty())
                {
                    this.Component.InformationIconPath = "/Images/picto-information.png";
                }

                this.Component.InformationIcon.ImageUrl = this.Component.InformationIconPath;
                this.Component.InformationIcon.ToolTipId = this.Component.GetInformationIconToolTipId;
                sbTagDateDivInnerHtml.Append(this.Component.InformationIcon.ToHtmlString());
            }

            if (this.Component.IsUpdatable)
            {
                //get consuming app area html
                if (!string.IsNullOrEmpty(this.Component.ConsumingAppAreaId))
                {
                    sbTagDateDivInnerHtml.Append(this.GetConsumingAppAreaHtml());
                }
            }

            //include error comp here to align it with the date
            //certain controls to be added only if the component is updatable
            if (this.Component.IsUpdatable && this.Component.ValidationString != null && this.Component.ValidationString.Length > 0)
            {
                this.Component.ValidationString.Clear();
                this.Component.ValidationString.Append(this.GetValidationSpan(this.Component.GetUpdatableDateTextName));
                this.Component.ErrorMessage = ErrorHelper.CreateErrorComponent(this.Component);
                sbTagDateDivInnerHtml.Append(this.Component.ErrorMessage?.ToString() ?? string.Empty);
                //  sbTagMainDivInnerHtml.Append(this.CreateErrorString(this.GetValidationSpan(this.Component.GetUpdatableDateTextName), this.Component.HtmlHelper));
            }

            if (this.Component.IsUpdatable)
            {
                if (!this.Component.DisplayTime)
                {
                    sbTagDateDivInnerHtml.Append(this.CreateHiddenTimeInputs());
                }
            }
            //if component is not updatable ,then create hidden inputs for keeping date and time as values for disabled controls is not submit
            else
            {
                if (this.Component.DisplayTime)
                {
                    sbTagDateDivInnerHtml.Append(this.CreateHiddenTimeInputs());
                }
            }

            tagBuilderDateDiv.InnerHtml.AppendHtml(sbTagDateDivInnerHtml.ToString());
            sbTagMainDivInnerHtml.Append(tagBuilderDateDiv);
            tagBuilderMainDiv.InnerHtml.AppendHtml(sbTagMainDivInnerHtml.ToString());
            writer.Write(tagBuilderMainDiv.ToString());
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
            if (this.Component.ValidationString == null || this.Component.ValidationString.Length == 0)
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
            return tagBuilderDate.ToString();
        }

        /// <summary>
        /// This adds validation attributes.
        /// </summary>
        private void AddValidationAttributesForDate()
        {
            this.Component.EnableValidationAttribute("dateformat");
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
            this.Component.AddValidationAttributeProperty("type", strType);
            this.Component.AddValidationAttributeProperty("format", format);
            this.Component.AddValidationAttributeProperty("defaultValue", DateTimeConstants.TimeDefaultValue);
            this.Component.AddValidationAttributeProperty("hourDropDown", this.Component.GetHourDropDownId);
            this.Component.AddValidationAttributeProperty("minuteDropDown", this.Component.GetMinuteDropDownId);
            this.Component.AddValidationAttributeProperty("msgIncorrectTime",
                string.Format(CultureInfo.CurrentCulture, ApplicationStrings.TimeMandatory, this.Component.ExternalLabelText));
            this.Component.AddValidationAttributeProperty("msgIncorrectDate",
                string.Format(CultureInfo.CurrentCulture, ApplicationStrings.ERR_DATE_INVALIDE, this.Component.ExternalLabelText));

            //add attribute if time is displayed or not
            var displayTime = this.Component.DisplayTime ? "true" : "false";
            this.Component.AddValidationAttributeProperty("displayTime", displayTime);
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
            return this.Component.EraseImage.ToHtmlString();
        }
    }
}
