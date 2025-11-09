// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompositeDateBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 09/06/2014
//   Author:  Sharma Siddharth (54626)
//   Description: Composite Date Builder Class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;

    /// <summary>
    /// The composite date builder.
    /// </summary>
    public class CompositeDateBuilder : ComponentBuilderBase<CompositeDateComponent, CompositeDateBuilder>
    {
        /// <summary>
        /// The custom label builder.
        /// </summary>
        private readonly CustomLabelBuilder customLabelBuilder;

        /// <summary>
        /// The information icon builder.
        /// </summary>
        private readonly ImageToolTipBuilder informationIconBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeDateBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="modelMetadata"></param>
        public CompositeDateBuilder(CompositeDateComponent component, ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
            this.customLabelBuilder = new CustomLabelBuilder(this.Component.CustomLabel, modelMetadata);
            this.informationIconBuilder = new ImageToolTipBuilder(this.Component.InformationIcon, modelMetadata);
            this.informationIconBuilder.ImageUrl("/Images/picto-information.png").CssClassImage("img15")
                .PersistanceMode(PersistanceMode.Click).Text(ApplicationStrings.MSG000491)
                .Title(ApplicationStrings.TIP000024).AlternateText(ApplicationStrings.TIP000024).Css("fortooltipclick")
                .CssClassSpan("help").CssClassInnerSpan("tooltip");
        }

        /// <summary>
        /// The custom label.
        /// </summary>
        /// <param name="setup">
        /// The setup.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder CustomLabel(Action<CustomLabelBuilder> setup)
        {
            if (setup == null)
            {
                return this;
            }

            setup(this.customLabelBuilder);
            return this;
        }

        /// <summary>
        /// The information icon.
        /// </summary>
        /// <param name="setup">
        /// The setup.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder InformationIcon(Action<ImageToolTipBuilder> setup)
        {
            if (setup == null)
            {
                return this;
            }

            setup(this.informationIconBuilder);
            return this;
        }

        /// <summary>
        /// The method to set CSS for the component.
        /// </summary>
        /// <param name="value">
        /// CSS class.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder CssMainDiv(string value)
        {
            this.Component.CssMainDiv = value;
            return this;
        }

        /// <summary>
        /// The first date id.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder FirstDateId(string value)
        {
            this.Component.FirstDate.Id = value;
            return this;
        }

        /// <summary>
        /// The second date id.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder SecondDateId(string value)
        {
            this.Component.SecondDate.Id = value;
            return this;
        }

        /// <summary>
        /// The date types included.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"
            , Justification = "TETHYS: The list values are to be provided by the user")]
        public CompositeDateBuilder DateTypesIncluded(List<EnumDateTypes> value)
        {
            this.Component.DateTypesIncluded = value;
            return this;
        }

        /// <summary>
        /// The first date and format.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder FirstDateAndFormat(DateTimeWithFormat value)
        {
            this.Component.FirstDateAndFormat = value;
            return this;
        }

        /// <summary>
        /// The second date and format.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder SecondDateAndFormat(DateTimeWithFormat value)
        {
            this.Component.FirstDateAndFormat = value;
            return this;
        }

        /// <summary>
        /// The first week and format.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder FirstWeekAndFormat(WeekYearWithFormat value)
        {
            this.Component.FirstWeekAndFormat = value;
            return this;
        }

        /// <summary>
        /// The second date and format.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder SecondWeekAndFormat(WeekYearWithFormat value)
        {
            this.Component.SecondWeekAndFormat = value;
            return this;
        }

        /// <summary>
        /// The first week id.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder FirstWeekId(string value)
        {
            this.Component.FirstWeek.Id = value;
            return this;
        }

        /// <summary>
        /// The second week id.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder SecondWeekId(string value)
        {
            this.Component.SecondWeek.Id = value;
            return this;
        }

        /// <summary>
        /// The data bind.
        /// </summary>
        /// <param name="dateTypesName"></param>
        /// <param name="dateTypesValue"></param>
        /// <param name="value"></param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = 
            "TETHYS: The list values are to be provided by the user.")]
        public CompositeDateBuilder DateTypes(string dateTypesName, string dateTypesValue, List<EnumDateTypes> value)
        {
            this.Component.DateTypesName = dateTypesName;
            this.Component.DateTypesValue = dateTypesValue;
            this.Component.DateTypesIncluded = value;
            return this;
        }

        /// <summary>
        /// The display time.
        /// </summary>
        /// <param name="value">
        /// This is used to display time.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder DisplayTime(bool value)
        {
            this.Component.DisplayTime = value;
            return this;
        }

        /// <summary>
        /// This sets the display erase button.
        /// </summary>
        /// <param name="value">
        /// The display erase button.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder DisplayEraseButton(bool value)
        {
            this.Component.DisplayEraseButton = value;
            return this;
        }

        /// <summary>
        /// Sets The CSS class date div.
        /// </summary>
        /// <param name="value">
        /// The CSS class date div.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder CssClassFirstDateDateDiv(string value)
        {
            this.Component.CssClassFirstDateDateDiv = value;
            return this;
        }

        /// <summary>
        /// The method to set CSS for the component first date.
        /// </summary>
        /// <param name="value">
        /// CSS class.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder CssFirstDateMainDiv(string value)
        {
            Component.CssFirstDateMainDiv = value;
            return this;
        }

        /// <summary>
        /// The method to set date change event for component first date.
        /// </summary>
        /// <param name="value">
        /// Date Change Event.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder OnFirstDateChange(string value)
        {
            Component.OnFirstDateChange = value;
            return this;
        }

        /// <summary>
        /// Sets The CSS class date div Second date.
        /// </summary>
        /// <param name="value">
        /// The CSS class date div.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder CssClassSecondDateDateDiv(string value)
        {
            this.Component.CssClassSecondDateDateDiv = value;
            return this;
        }

        /// <summary>
        /// The method to set CSS for the component Second date.
        /// </summary>
        /// <param name="value">
        /// CSS class.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder CssSecondDateMainDiv(string value)
        {
            Component.CssSecondDateMainDiv = value;
            return this;
        }

        /// <summary>
        /// The method to set date change event for component Second date.
        /// </summary>
        /// <param name="value">
        /// Date Change Event.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder OnSecondDateChange(string value)
        {
            Component.OnSecondDateChange = value;
            return this;
        }

        /// <summary>
        /// Sets The CSS class week div.
        /// </summary>
        /// <param name="value">
        /// The CSS class week div.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder CssClassFirstWeekWeekDiv(string value)
        {
            this.Component.CssClassFirstWeekWeekDiv = value;
            return this;
        }

        /// <summary>
        /// The method to set CSS for the component first week.
        /// </summary>
        /// <param name="value">
        /// CSS class.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder CssFirstWeekMainDiv(string value)
        {
            Component.CssFirstWeekMainDiv = value;
            return this;
        }

        /// <summary>
        /// The method to set week change event for component first week.
        /// </summary>
        /// <param name="value">
        /// Date Change Event.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder OnFirstWeekChange(string value)
        {
            Component.OnFirstWeekChange = value;
            return this;
        }

        /// <summary>
        /// Sets The CSS class week div.
        /// </summary>
        /// <param name="value">
        /// The CSS class week div.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder CssClassSecondWeekWeekDiv(string value)
        {
            this.Component.CssClassSecondWeekWeekDiv = value;
            return this;
        }

        /// <summary>
        /// The method to set CSS for the component Second week.
        /// </summary>
        /// <param name="value">
        /// CSS class.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder CssSecondWeekMainDiv(string value)
        {
            Component.CssSecondWeekMainDiv = value;
            return this;
        }

        /// <summary>
        /// The CSS class date types select div.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder CssClassDateTypesSelectDiv(string value)
        {
            Component.CssClassDateTypesSelectDiv = value;
            return this;
        }

        /// <summary>
        /// The CSS class label div.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder CssClassLabelDiv(string value)
        {
            Component.CssClassLabelDiv = value;
            return this;
        }

        /// <summary>
        /// The method to set week change event for component Second week.
        /// </summary>
        /// <param name="value">
        /// Date Change Event.
        /// </param>
        /// <returns>
        /// The <see cref="CompositeDateBuilder"/>.
        /// </returns>
        public CompositeDateBuilder OnSecondWeekChange(string value)
        {
            Component.OnSecondWeekChange = value;
            return this;
        }
    }
}
