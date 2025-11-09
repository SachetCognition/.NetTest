// -------------------------------------------------------------------------------------------------
// <copyright file="DateTimeComponent.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 07/04/2014
//    Author:  Sharma Siddharth (54626) 
//    Description: The component class for the DateTime component
// </summary>
// -------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.Common.Resources;
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

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The component class for the DateTime component
    /// </summary>
    public class DateTimeComponent : ComponentBase
    {
        
        /// <summary>
        /// The date and format.
        /// </summary>
        private DateTimeWithFormat dateValue;

        /// <summary>
        /// The on date change.
        /// </summary>
        private string onDateChange;

        /// <summary>
        /// The custom label.
        /// </summary>
        public CustomLabelComponent CustomLabel { get; set; }

        /// <summary>
        /// the drop down list for hours.
        /// </summary>
        internal DropDownListComponent DropDownListHour { get; set; }

        /// <summary>
        /// the drop down list for minutes.
        /// </summary>
        internal DropDownListComponent DropDownListMinute { get; set; }

        /// <summary>
        /// Gets or sets the consuming app area.
        /// </summary>
        internal LabelComponent ConsumingAppArea { get; set; }

        /// <summary>
        /// Gets or sets the consuming app area id.
        /// </summary>
        public string ConsumingAppAreaId { get; set; }

        /// <summary>
        /// The label for colon between hour and minute.
        /// </summary>
        internal LabelComponent LabelColon { get; set; }

        /// <summary>
        /// The erase image.
        /// </summary>
        internal HyperLinkComponent EraseImage { get; set; }

        /// <summary>
        /// The current date image.
        /// </summary>
        internal HyperLinkComponent CurrentDateImage { get; set; }

        /// <summary>
        /// Gets or sets the label date read only.
        /// </summary>
        internal LabelComponent LabelDateReadOnly { get; set; }

        /// <summary>
        /// Gets or sets the information icon.
        /// </summary>
        public ImageToolTipComponent InformationIcon { get; set; }

        /// <summary>
        /// get or set the path of the current date selection image url.
        /// </summary>
        public string CurrentDateSelectionImageUrl { get; set; }

        /// <summary>
        /// get or set the path of the current date selection title.
        /// </summary>
        public string CurrentDateSelectionTitle { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeComponent"/> class.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        public DateTimeComponent(IHtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            //set default values for properties
            this.onDateChange = "null";
            this.CustomLabel = new CustomLabelComponent(this.HtmlHelper);
            this.DisplayTime = true;
            this.DisplayEraseButton = false;
            this.DisplayInformationIcon = false;
            this.dateValue = new DateTimeWithFormat(DateTimeConstants.EnglishFormat, false);
            this.AssociatedDateHtmlId = string.Empty;
            this.DisplayCurrentDateSelector = false;

            //initialize all components so that their JS can be included later in this method.
            this.InformationIcon = new ImageToolTipComponent(this.HtmlHelper);
            this.DropDownListHour = new DropDownListComponent(this.HtmlHelper);
            this.EraseImage = new HyperLinkComponent(this.HtmlHelper);
            this.LabelColon = new LabelComponent(this.HtmlHelper);
            this.LabelDateReadOnly = new LabelComponent(this.HtmlHelper);
            this.DropDownListMinute = new DropDownListComponent(this.HtmlHelper);
            this.ConsumingAppArea = new LabelComponent(this.HtmlHelper);
            this.CurrentDateImage = new HyperLinkComponent(this.HtmlHelper);
            this.StartFromCurrentDate = false;         
        }

        /// <summary>
        /// Gets the JS resources.
        /// </summary>
        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {

                var jsRes = new List<JsResource>
                           {
                               new JsResource("JsDatePicker", "Equant.SAV2000.ComponentLibrary.Common.Resources.Javascripts.jquery-ui-datepicker.js", 210,
                                   typeof(Locator)),
                               new JsResource("JsDateTimeCommon", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DateTimeCommon.js", 220,
                                   typeof(DateTimeComponent)),
                               new JsResource("JsDateTime", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DateTime.js", 230,
                                   typeof(DateTimeComponent))
                           };

                if (this.DisplayInformationIcon && this.InformationIcon != null  && !string.IsNullOrEmpty(this.InformationIcon.Text))
                {
                    jsRes.AddRange(this.InformationIcon.JsResources);
                }

                if (this.DisplayTime && (this.DropDownListHour != null ))
                {
                  jsRes.AddRange(this.DropDownListHour.JsResources);
                }

                if (this.DisplayTime && this.DropDownListMinute != null)
                {
                    jsRes.AddRange(this.DropDownListMinute.JsResources);
                    
                }


                if (this.IsUpdatable && this.DisplayEraseButton && this.EraseImage != null)
                {
                    jsRes.AddRange(this.EraseImage.JsResources);

                }


                if (this.IsUpdatable && this.DisplayCurrentDateSelector && this.CurrentDateImage != null)
                {
                    jsRes.AddRange(this.CurrentDateImage.JsResources);

                }

               if (this.ErrorMessage != null)
               {
                   jsRes.AddRange(this.ErrorMessage.JsResources);
               }

                return new ReadOnlyCollection<JsResource>(jsRes);
                
            }
        }

        /// <summary>
        /// Gets or sets the CSS class of the anchor tag
        /// </summary>
        public string CssMainDiv { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for date input control.
        /// </summary>
        public string CssClassDateInput { get; set; }

        /// <summary>
        /// Gets or sets the on date change.
        /// </summary>
        public string OnDateChange
        {
            get
            {
                return this.onDateChange;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.onDateChange = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public ErrorComponents ErrorMessage { get; set; }

      /// <summary>
        /// Gets or sets the erase image path.
        /// </summary>
        public string EraseImagePath { get; set; }

        /// <summary>
        /// Gets or sets the information icon path.
        /// </summary>
        public string InformationIconPath { get; set; }

        /// <summary>
        /// Gets or sets the calendar image path.
        /// </summary>
        public string CalendarImagePath { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for label div.
        /// </summary>
        public string CssClassLabelDiv { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for date div.
        /// </summary>
        public string CssClassDateDiv { get; set; }

        /// <summary>
        /// The value to be submit to controller if it is a DateTime mode
        /// </summary>
        public DateTimeWithFormat Value
        {
            get
            {
                return this.dateValue;
            }

            set
            {
                if (value != null)
                {
                    this.dateValue = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether display time.
        /// </summary>
        public bool DisplayTime { get; set; }

        /// <summary>
        /// Gets or sets the erase button text.
        /// </summary>
        public string EraseButtonText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether display erase button.
        /// </summary>
        public bool DisplayEraseButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to display current date selector.
        /// </summary>
        public bool DisplayCurrentDateSelector { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether display information icon.
        /// </summary>
        public bool DisplayInformationIcon { get; set; }

        /// <summary>
        /// Gets or sets the label CSS read only.
        /// </summary>
        public string LabelCssReadOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether date shall start from current date.
        /// </summary>
        public bool StartFromCurrentDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether associated date html id.
        /// </summary>
        public string AssociatedDateHtmlId { get; set; }

        /// <summary>
        /// Gets and sets the value of hour label text.
        /// </summary>
        public string AccessHrText { get; set; }

        /// <summary>
        /// Gets and sets the value of minute label text.
        /// </summary>
        public string AccessMinText { get; set; }   

        /// <summary>
        /// The get drop down hour name.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHourDropDownId
        {
            get
            {
                var sbDdlHourId = new StringBuilder(this.Id);
                sbDdlHourId.Append("DropDownHours");
                return sbDdlHourId.ToString();
            }
        }

        /// <summary>
        /// Gets the get date textbox id.
        /// </summary>
        public string GetDateTextId
        {
            get
            {
                return this.Id.AppendWithBuilder("Date");
            }
        }

        /// <summary>
        /// Gets the get updatable date text name.
        /// </summary>
        public string GetUpdatableDateTextName
        {
            get
            {
                return this.Name.AppendWithBuilder(".Date");
            }
        }

        /// <summary>
        /// Gets the date type id.
        /// </summary>
        public string DateTypeId
        {
            get
            {
                return this.Id.AppendWithBuilder("Type");
            }
        }

        /// <summary>
        /// Gets the get erase button id.
        /// </summary>
        public string GetEraseButtonId
        {
            get
            {
                return this.Id.AppendWithBuilder("EraseImage");
            }
        }

        /// <summary>
        /// Gets the current date image id.
        /// </summary>
        public string GetCurrentDateImageId
        {
            get
            {
                return this.Id.AppendWithBuilder("CurrentDateImage");
            }
        }

        /// <summary>
        /// Gets the get information icon Name.
        /// </summary>
        public string GetInformationIconName
        {
            get
            {
                return this.Name.AppendWithBuilder("InformationIcon");
            }
        }

        /// <summary>
        /// Gets the get information icon id.
        /// </summary>
        public string GetInformationIconId
        {
            get
            {
                return this.Id.AppendWithBuilder("InformationIcon");
            }
        }

        /// <summary>
        /// this returns the information icon tool tip id.
        /// </summary>
        public string GetInformationIconToolTipId
        {
            get
            {
                return this.Id.AppendWithBuilder("InformationIconToolTip");
            }
        }

        /// <summary>
        /// The get hour drop down name.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetUpdatableHourDropDownName
        {
            get
            {
                return this.Name.AppendWithBuilder(".DropDownHours");
            }
        }

        /// <summary>
        /// The get hour drop down name.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetReadOnlyHourDropDownName
        {
            get
            {
                return this.Name.AppendWithBuilder("DropDownHoursReadonly");
            }
        }

        /// <summary>
        /// The get min drop down name.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetMinuteDropDownId
        {
            get
            {
                var sbDdlMinName = new StringBuilder(this.Id);
                sbDdlMinName.Append("DropDownMins");
                return sbDdlMinName.ToString();
            }
        }

        /// <summary>
        /// The get min drop down name.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetUpdatableMinDropDownName
        {
            get
            {
                return this.Name.AppendWithBuilder(".DropDownMins");
            }
        }

        /// <summary>
        /// The get min drop down name.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetReadOnlyMinDropDownName
        {
            get
            {
                return this.Name.AppendWithBuilder("DropDownMinsReadOnly");
            }
        }

        /// <summary>
        /// Gets the main div id.
        /// </summary>
        public string MainDivId
        {
            get
            {
                return this.Id.AppendWithBuilder("MainDiv");
            }
        }

        /// <summary>
        /// Gets the hidden hour id.
        /// </summary>
        public string HiddenHourId
        {
            get
            {
                return this.Id.AppendWithBuilder("HourHidden");
            }
        }

        /// <summary>
        /// Gets the hidden minute id.
        /// </summary>
        public string HiddenMinuteId
        {
            get
            {
                return this.Id.AppendWithBuilder("MinuteHidden");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is mandatory.
        /// </summary>
        public bool IsMandatory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the component is updatable.
        /// </summary>
        public bool IsUpdatable { get; set; }

        /// <summary>
        /// Gets or sets the conditional annotations.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification =
            "TETHYS: This input is required.")]
        public Dictionary<string, string> ConditionalAnnotations { get; set; }

        /// <summary>
        /// Gets or sets the mandatory message.
        /// </summary>
        public string MandatoryMessage { get; set; }

        /// <summary>
        /// Gets or sets the tool tip text.
        /// </summary>
        public string ExternalLabelText { get; set; }

        /// <summary>
        /// The write html.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void WriteHtml(TextWriter writer)
        {
            new DateTimeHtmlBuilder(this).Build(writer);
        }

        /// <summary>
        /// This writes initial start up script.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void WriteInitScript(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentException("The parameter writer cannot be null");
            }

            // Set calender tooltip
            var calendarText = string.Format(CultureInfo.CurrentCulture, ApplicationStrings.TIP000001, this.ExternalLabelText);

            // Set calender image path

            var calendarImagePath = string.IsNullOrEmpty(this.CalendarImagePath) ? "/Images/calendar.png" : this.CalendarImagePath;

            string jsDateFormat;

            switch (this.Value.Format)
            {
                case DateTimeConstants.FrenchFormat:
                    jsDateFormat = DateTimeConstants.JsFrenchFormat;
                    break;
                default:
                    jsDateFormat = DateTimeConstants.JsEnglishFormat;
                    break;
            }

            var options =
                JsonConvert.SerializeObject(
                    new
                    {
                        onDateChange = new JRaw(this.OnDateChange),
                        calendarImagePath,
                        calendarImageText = calendarText,
                        //at this moment language is set using format, but in Oceane four languages are supported. Therefore this logic will be revisited
                        // to set the language according to the user's language which may be received as input from the user or from current culture
                        culture = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString(),
                        dateFormat = jsDateFormat.ToLower(CultureInfo.CurrentCulture),
                        ddlHourId = this.GetHourDropDownId,
                        ddlMinuteId = this.GetMinuteDropDownId,
                        txtDateId = this.GetDateTextId,
                        displayTime = this.DisplayTime,
                        eraseButtonId = this.GetEraseButtonId,
                        currentDateImageId = this.GetCurrentDateImageId,
                        timeDefaultValue = DateTimeConstants.TimeDefaultValue,
                        isUpdatable = this.IsUpdatable,
                        startFromCurrentDate = this.StartFromCurrentDate,
                        associatedDateHtmlId = this.AssociatedDateHtmlId,
                        dateTypeId = this.DateTypeId,
                        mainDivId = this.MainDivId,
                        hiddenHourId = this.HiddenHourId,
                        hiddenMinuteId = this.HiddenMinuteId,
                        timeZoneOffset = DateComponentHelper.ResolveOffset(this.Value.TimeOffset),
                        utcMode = this.Value.IsUtcMode,
                        dateDivId = this.Id
                    });

            writer.WriteLine("$('#{0}').dateTime({1});", this.Id, options);

           
            //write init script for information icon
            if (this.DisplayInformationIcon && this.InformationIcon != null
                && !string.IsNullOrEmpty(this.InformationIcon.Text))
            {
                
                this.InformationIcon.WriteInitScript(writer);
            }

            //write for dropDown
            if (this.DisplayTime && this.DropDownListHour != null)
            {
                this.DropDownListHour.WriteInitScript(writer);
            }

            if (this.DisplayTime && this.DropDownListMinute != null)
            {
               this.DropDownListMinute.WriteInitScript(writer);
            }

            //write init script for erase button
            if (this.IsUpdatable && this.DisplayEraseButton && this.EraseImage != null)
            {
               this.EraseImage.WriteInitScript(writer);
            }

            //write init script for current date selector image
            if (this.IsUpdatable && this.DisplayCurrentDateSelector && this.CurrentDateImage != null)
            {
                this.CurrentDateImage.WriteInitScript(writer);
            }

            if (this.ErrorMessage != null)
            {
                this.ErrorMessage.WriteInitScript(writer);
            }
        }
    }
}
