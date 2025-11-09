// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompositeDateComponent.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 09/06/2014
//   Author:  Sharma Siddharth (54626)
//   Description: Component Class for the Composite Date Control
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Web.Mvc;
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Label;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

    using Newtonsoft.Json;

    /// <summary>
    /// This is used to set a value which will decide which of the date controls shall be rendered.
    /// This is different from the "DateVisibility" enumeration.
    /// This enumeration decides if the date shall be/not be rendered itself.
    /// </summary>
    public enum DateRenderer
    {
        //This will skip all the dates
        SkipBothDates = 0,

        //Render both Dates
        RenderBothDates = 1,

        //this skips second date
        SkipSecondDate = 2,
    }

    /// <summary>
    /// This is used to set a value which will decide which of the week controls shall be rendered.
    /// This enumeration decides if the week shall be/not be rendered itself.
    /// </summary>
    public enum WeekRenderer
    {
        //this skips both weeks
        SkipBothWeeks = 0,

        //This renders both weeks
        RenderBothWeeks = 1,

        //this skips second week
        SkipSecondWeek = 2
    }

    /// <summary>
    /// This is used to set visibility for dates. The DateVisibility enumeration decides if the date shall be hidden when the control
    ///  is initially rendered.
    /// </summary>
    public enum DateVisibility
    {
        //This will hide both the dates
        HideBothDates = 0,

        //This shows both Dates
        ShowBothDates = 1,

        //this hides second date
        HideSecondDate = 2
    }

    /// <summary>
    /// This is used to set visibility for weeks. The WeekVisibility enumeration decides if the weeks shall be hidden when the control
    ///  is initially rendered.
    /// </summary>
    public enum WeekVisibility
    {
        //This will hide both the dates
        HideBothWeeks = 0,

        //This shows both Dates
        ShowBothWeeks = 1,

        //this hides second date
        HideSecondWeek = 2
    }

    /// <summary>
    /// The composite date component.
    /// </summary>
    public class CompositeDateComponent : ComponentBase
    {

        /// <summary>
        /// The first date.
        /// </summary>
        private DateTimeComponent firstDate;

        /// <summary>
        /// The second date.
        /// </summary>
        private DateTimeComponent secondDate;

        /// <summary>
        /// The first week.
        /// </summary>
        private WeekYearComponent firstWeek;

        /// <summary>
        /// The second week.
        /// </summary>
        private WeekYearComponent secondWeek;

        /// <summary>
        /// Gets the JS resources.
        /// </summary>
        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {

                var jsRes = new List<JsResource>
                           {
                               new JsResource("JsCompositeDate", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CompositeDate.js", 200, 
                                   typeof(CompositeDateComponent))
                           };

                if (!string.IsNullOrEmpty(this.InformationIcon.Text))
                {
                    jsRes.AddRange(this.InformationIcon.JsResources);
                }

                if (this.DropDownDateTypes != null)
                {
                    jsRes.AddRange(this.DropDownDateTypes.JsResources);
                }

                var renderFirstDate = (this.DatesToRender != DateRenderer.SkipBothDates) && (this.FirstDate != null);
                var renderSecondDate = (this.DatesToRender == DateRenderer.RenderBothDates) && (this.SecondDate != null);
                var renderFirstWeek = (this.WeeksToRender != WeekRenderer.SkipBothWeeks) && (this.FirstWeek != null);
                var renderSecondWeek = (this.WeeksToRender == WeekRenderer.RenderBothWeeks) && (this.SecondWeek != null);

                if (renderFirstDate)
                {
                    jsRes.AddRange(this.FirstDate.JsResources);
                }


                if (renderSecondDate)
                {
                    jsRes.AddRange(this.SecondDate.JsResources);
                }

                if (renderFirstWeek)
                {
                    jsRes.AddRange(this.FirstWeek.JsResources);

                }

                if (renderSecondWeek)
                {
                    jsRes.AddRange(this.SecondWeek.JsResources);
                }

                if (!(string.IsNullOrEmpty(this.CustomLabel.Text)) || !(string.IsNullOrEmpty(this.AndLabel.Text )))
                {
                    jsRes.AddRange(this.CustomLabel.JsResources);
                } 
                
                return new ReadOnlyCollection<JsResource>(jsRes);

            }
        }

        /// <summary>
        /// Gets or sets the date types name.
        /// </summary>
        public string DateTypesName { get; set; }

        /// <summary>
        /// Gets or sets the date types value.
        /// </summary>
        public string DateTypesValue { get; set; }

        /// <summary>
        /// The custom label.
        /// </summary>
        public CustomLabelComponent CustomLabel { get; set; }

        /// <summary>
        /// Gets or sets the information icon.
        /// </summary>
        public ImageToolTipComponent InformationIcon { get; set; }

        /// <summary>
        /// Gets or sets the first date and format.
        /// </summary>
        public DateTimeWithFormat FirstDateAndFormat
        {
            get
            {
                return this.FirstDate.Value;
            }

            set
            {
                this.FirstDate.Value = value;
            }
        }

        /// <summary>
        /// Gets or sets the second date and format.
        /// </summary>
        public DateTimeWithFormat SecondDateAndFormat
        {
            get
            {
                return this.SecondDate.Value;
            }

            set
            {
                this.SecondDate.Value = value;
            }
        }

        /// <summary>
        /// Gets or sets the second week and format.
        /// </summary>
        public WeekYearWithFormat SecondWeekAndFormat
        {
            get
            {
                return this.SecondWeek.Value;
            }

            set
            {
                this.SecondWeek.Value = value;
            }
        }

        /// <summary>
        /// Gets or sets the first week and format.
        /// </summary>
        public WeekYearWithFormat FirstWeekAndFormat
        {
            get
            {
                return this.FirstWeek.Value;
            }

            set
            {
                this.FirstWeek.Value = value;
            }
        }

        /// <summary>
        /// Gets or sets the drop down date types.
        /// </summary>
        internal DropDownListComponent DropDownDateTypes { get; set; }

        /// <summary>
        /// Gets or sets the first date builder.
        /// </summary>
        internal DateTimeBuilder FirstDateBuilder { get; set; }

        /// <summary>
        /// Gets or sets the second date builder.
        /// </summary>
        internal DateTimeBuilder SecondDateBuilder { get; set; }

        /// <summary>
        /// Gets or sets the first week builder.
        /// </summary>
        internal WeekYearBuilder FirstWeekBuilder { get; set; }

        /// <summary>
        /// Gets or sets the second week builder.
        /// </summary>
        internal WeekYearBuilder SecondWeekBuilder { get; set; }

        /// <summary>
        /// Gets or sets the first date.
        /// </summary>
        internal DateTimeComponent FirstDate
        {
            get
            {
                return this.firstDate;
            }

            set
            {
                if (value != null)
                {
                    this.firstDate = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the second date.
        /// </summary>
        internal DateTimeComponent SecondDate
        {
            get
            {
                return this.secondDate;
            }

            set
            {
                if (value != null)
                {
                    this.secondDate = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the first week.
        /// </summary>
        internal WeekYearComponent FirstWeek
        {
            get
            {
                return this.firstWeek;
            }

            set
            {
                if (value != null)
                {
                    this.firstWeek = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the second week.
        /// </summary>
        internal WeekYearComponent SecondWeek
        {
            get
            {
                return this.secondWeek;
            }

            set
            {
                if (value != null)
                {
                    this.secondWeek = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the and span.
        /// </summary>
        internal LabelComponent AndLabel { get; set; }

        /// <summary>
        /// Gets the and label id.
        /// </summary>
        internal string AndLabelId
        {
            get
            {
                return this.Id.AppendWithBuilder("AndLabel");
            }
        }

        /// <summary>
        /// Gets the id for Control custom label.
        /// </summary>
        internal string ControlLabelId
        {
            get
            {
                return this.Id.AppendWithBuilder("labelForDate");
            }
        }     

        /// <summary>
        /// Gets or sets the date types which are to be included to display the date.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = 
            "TETHYS: This input is required."), 
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = 
            "TETHYS: The list values are to be provided by the user.")]
        public List<EnumDateTypes> DateTypesIncluded { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the anchor tag
        /// </summary>
        public string CssMainDiv { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for custom Label div.
        /// </summary>
        public string CssClassLabelDiv { get; set; }

        /// <summary>
        /// Gets or sets the CSS class and label.
        /// </summary>
        public string CssClassAndLabel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether display erase button.
        /// </summary>
        public bool DisplayEraseButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether display time.
        /// </summary>
        public bool DisplayTime { get; set; }

        /// <summary>
        /// Gets or sets the on date change.
        /// </summary>
        public string OnFirstDateChange { get; set; }

        /// <summary>
        /// Gets or sets the on date change.
        /// </summary>
        public string OnSecondDateChange { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the anchor tag
        /// </summary>
        public string CssFirstDateMainDiv { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for date div.
        /// </summary>
        public string CssClassFirstDateDateDiv { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the anchor tag
        /// </summary>
        public string CssSecondDateMainDiv { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for date div.
        /// </summary>
        public string CssClassSecondDateDateDiv { get; set; }

        /// <summary>
        /// Gets or sets the CSS class first week week div.
        /// </summary>
        public string CssClassFirstWeekWeekDiv { get; set; }

        /// <summary>
        /// Gets or sets the CSS first week main div.
        /// </summary>
        public string CssFirstWeekMainDiv { get; set; }

        /// <summary>
        /// Gets or sets the on first week change.
        /// </summary>
        public string OnFirstWeekChange { get; set; }

        /// <summary>
        /// Gets or sets the CSS class Second week week div.
        /// </summary>
        public string CssClassSecondWeekWeekDiv { get; set; }

        /// <summary>
        /// Gets or sets the CSS class date types select div.
        /// </summary>
        public string CssClassDateTypesSelectDiv { get; set; }

        /// <summary>
        /// Gets or sets the CSS Second week main div.
        /// </summary>
        public string CssSecondWeekMainDiv { get; set; }

        /// <summary>
        /// Gets or sets the on Second week change.
        /// </summary>
        public string OnSecondWeekChange { get; set; }

        /// <summary>
        /// Gets the date types id.
        /// </summary>
        internal string DateTypesId
        {
            get
            {
                return this.Id.AppendWithBuilder("DdlDateTypes");
            }
        }

        /// <summary>
        /// Gets or sets the dates to render.
        /// </summary>
        public DateRenderer DatesToRender { get; set; }

        /// <summary>
        /// Gets or sets the weeks to render.
        /// </summary>
        public WeekRenderer WeeksToRender { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether render and label.
        /// </summary>
        public bool RenderAndLabel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeDateComponent"/> class.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        public CompositeDateComponent(IHtmlHelper htmlHelper)
            : this(htmlHelper, null, null, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeDateComponent"/> class.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="firstDateBuilder"></param>
        /// <param name="secondDateBuilder"></param>
        /// <param name="firstWeekBuilder"></param>
        /// <param name="secondWeekBuilder"></param>
        public CompositeDateComponent(IHtmlHelper htmlHelper, DateTimeBuilder firstDateBuilder, DateTimeBuilder secondDateBuilder ,
            WeekYearBuilder firstWeekBuilder, WeekYearBuilder secondWeekBuilder)
            : base(htmlHelper)
        {
            //initialize all components so that their JS can be included later in this method.
            this.CustomLabel = new CustomLabelComponent(this.HtmlHelper);
            this.InformationIcon = new ImageToolTipComponent(this.HtmlHelper);
            this.DropDownDateTypes = new DropDownListComponent(this.HtmlHelper);
            this.AndLabel = new LabelComponent(this.HtmlHelper);
            if (firstDateBuilder != null)
            {
                this.FirstDateBuilder = firstDateBuilder;
                this.FirstDate = this.FirstDateBuilder.Component ?? new DateTimeComponent(this.HtmlHelper);
            }
            else
            {
                this.FirstDate = new DateTimeComponent(this.HtmlHelper);
                this.FirstDateBuilder = new DateTimeBuilder(this.FirstDate, this.ModelMetadata);
            }

            if (secondDateBuilder != null)
            {
                this.SecondDateBuilder = secondDateBuilder;
                this.SecondDate = this.SecondDateBuilder.Component ?? new DateTimeComponent(this.HtmlHelper);
            }
            else
            {
                this.SecondDate = new DateTimeComponent(this.HtmlHelper);
                this.SecondDateBuilder = new DateTimeBuilder(this.SecondDate, this.ModelMetadata);
            }

            if (firstWeekBuilder != null)
            {
                this.FirstWeekBuilder = firstWeekBuilder;
                this.FirstWeek = this.FirstWeekBuilder.Component ?? new WeekYearComponent(this.HtmlHelper);
            }
            else
            {
                this.FirstWeek = new WeekYearComponent(this.HtmlHelper);
                this.FirstWeekBuilder = new WeekYearBuilder(this.FirstWeek, this.ModelMetadata);
            }

            if (secondWeekBuilder != null)
            {
                this.SecondWeekBuilder = secondWeekBuilder;
                this.SecondWeek = this.SecondWeekBuilder.Component ?? new WeekYearComponent(this.HtmlHelper);
            }
            else
            {
                this.SecondWeek = new WeekYearComponent(this.HtmlHelper);
                this.SecondWeekBuilder = new WeekYearBuilder(this.SecondWeek, this.ModelMetadata);
            }

            this.DisplayTime = true;
            this.DisplayEraseButton = true;

            
        }

        /// <summary>
        /// The write html.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void WriteHtml(TextWriter writer)
        {
            new CompositeDateHtmlBuilder(this).Build(writer);
        }

        /// <summary>
        /// The write initial script.
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

           //write init script for information icon
            if (!string.IsNullOrEmpty(this.InformationIcon.Text))
            {
                this.InformationIcon.WriteInitScript(writer);
            }

            //write for dropDown
            if (this.DropDownDateTypes != null)
            {
                this.DropDownDateTypes.WriteInitScript(writer);
            }

            var renderFirstDate = (this.DatesToRender != DateRenderer.SkipBothDates) && (this.FirstDate != null);
            var renderSecondDate = (this.DatesToRender == DateRenderer.RenderBothDates) && (this.SecondDate != null);
            var renderFirstWeek = (this.WeeksToRender != WeekRenderer.SkipBothWeeks) && (this.FirstWeek != null);
            var renderSecondWeek = (this.WeeksToRender == WeekRenderer.RenderBothWeeks) && (this.SecondWeek != null);
            if (renderFirstDate)
            {
                this.FirstDate.WriteInitScript(writer);
            }

            if (renderSecondDate)
            {
                this.SecondDate.WriteInitScript(writer);
            }

            if (renderFirstWeek)
            {
                this.FirstWeek.WriteInitScript(writer);
            }

            if (renderSecondWeek)
            {
                this.SecondWeek.WriteInitScript(writer);
            }

            var options =
                JsonConvert.SerializeObject(new
                {
                    ddlDateTypesId = this.DateTypesId,
                    firstDateId = renderFirstDate ? this.FirstDate.Id : string.Empty,
                    secondDateId = renderSecondDate ? this.SecondDate.Id : string.Empty,
                    firstWeekId = renderFirstWeek ? this.FirstWeek.Id : string.Empty,
                    secondWeekId = renderSecondWeek ? this.SecondWeek.Id : string.Empty,
                    andLabelId = this.RenderAndLabel ? this.AndLabelId : string.Empty,
                    controlLabelId = this.ControlLabelId,
                    firstDateTextId = renderFirstDate ? this.FirstDate.GetDateTextId : string.Empty,
                    firstWeekTextId = renderFirstWeek ? this.FirstWeek.WeekTextId : string.Empty
                });

            writer.WriteLine("$('#{0}').compositeDate({1});", this.Id, options);
        }
    }
}
