// -------------------------------------------------------------------------------------------------
// <copyright file="DateTimeBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 07/04/2014
//    Author:  Sharma Siddharth (54626) 
//    Description: The Builder class for the DateTime component
// </summary>
// -------------------------------------------------------------------------------------------------
namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;

    /// <summary>
    /// The Builder class for the DateTime component
    /// </summary>
    public class DateTimeBuilder : ComponentBuilderBase<DateTimeComponent, DateTimeBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="modelMetadata"></param>
        public DateTimeBuilder(DateTimeComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
            this.customLabelBuilder = new CustomLabelBuilder(this.Component.CustomLabel, modelMetadata);
            this.informationIconBuilder = new ImageToolTipBuilder(this.Component.InformationIcon, modelMetadata)
                .ImageUrl("/Images/picto-information.png").CssClassImage("img15").PersistanceMode(PersistanceMode.Click)
                .Text(ApplicationStrings.DateImageToolTip).Title(ApplicationStrings.TIP000024)
                .AlternateText(ApplicationStrings.TIP000024).Css("fortooltipclick").CssClassSpan("help").CssClassInnerSpan("tooltip");
        }

        /// <summary>
        /// The custom label builder.
        /// </summary>
        private readonly CustomLabelBuilder customLabelBuilder;

        /// <summary>
        /// The information icon builder.
        /// </summary>
        private readonly ImageToolTipBuilder informationIconBuilder;

        /// <summary>
        /// The custom label.
        /// </summary>
        /// <param name="setup">
        /// The setup.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder CustomLabel(Action<CustomLabelBuilder> setup)
        {
            if (setup == null)
            {
                return this;
            }

            setup(this.customLabelBuilder);
            return this;
        }

        /// <summary>
        /// The consuming app area id.
        /// </summary>
        /// <param name="appAreaId">
        /// The app area id.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder ConsumingAppAreaId(string appAreaId)
        {
            this.Component.ConsumingAppAreaId = appAreaId;
            return this;
        }

        /// <summary>
        /// The information icon.
        /// </summary>
        /// <param name="setup">
        /// The setup.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder InformationIcon(Action<ImageToolTipBuilder> setup)
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
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder CssMainDiv(string value)
        {
            Component.CssMainDiv = value;
            return this;
        }

        /// <summary>
        /// The method to set date change event for component.
        /// </summary>
        /// <param name="value">
        /// Date Change Event.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder OnDateChange(string value)
        {
            Component.OnDateChange = value;
            return this;
        }

        /// <summary>
        /// The method to set value for component.
        /// </summary>
        /// <param name="date">
        /// Title text for component.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder Value(DateTimeWithFormat date)
        {
            Component.Value = date;
            return this;
        }
        
        /// <summary>
        /// The display time.
        /// </summary>
        /// <param name="value">
        /// This is used to display time.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder DisplayTime(bool value)
        {
            this.Component.DisplayTime = value;
            return this;
        }

        /// <summary>
        /// Set the text to assign to erase button.
        /// </summary>
        /// <param name="value">
        /// The erase button text.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder EraseButtonText(string value)
        {
            this.Component.EraseButtonText = value;
            return this;
        }

        /// <summary>
        /// This sets the display erase button.
        /// </summary>
        /// <param name="value">
        /// The display erase button.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder DisplayEraseButton(bool value)
        {
            this.Component.DisplayEraseButton = value;
            return this;
        }

        /// <summary>
        /// This sets the display information icon.
        /// </summary>
        /// <param name="value">
        /// The display information icon.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder DisplayInformationIcon(bool value)
        {
            this.Component.DisplayInformationIcon = value;
            return this;
        }

        /// <summary>
        /// Sets The CSS class label div.
        /// </summary>
        /// <param name="value">
        /// The CSS class label div.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder CssClassLabelDiv(string value)
        {
            this.Component.CssClassLabelDiv = value;
            return this;
        }

        /// <summary>
        /// Sets The CSS class date div.
        /// </summary>
        /// <param name="value">
        /// The CSS class date div.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder CssClassDateDiv(string value)
        {
            this.Component.CssClassDateDiv = value;
            return this;
        }

        /// <summary>
        /// The CSS class date input.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder CssClassDateInput(string value)
        {
            this.Component.CssClassDateInput = value;
            return this;
        }

        /// <summary>
        /// Sets the label CSS for readonly mode.
        /// </summary>
        /// <param name="value">
        /// The label CSS read only.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public DateTimeBuilder LabelCssReadOnly(string value)
        {
            this.Component.LabelCssReadOnly = value;
            return this;
        }

        /// <summary>
        /// This sets if date shall start from current date.
        /// </summary>
        /// <param name="value">
        /// The start from current date.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder StartFromCurrentDate(bool value)
        {
            this.Component.StartFromCurrentDate = value;
            return this;
        }

        /// <summary>
        /// This sets associated date html id.
        /// </summary>
        /// <param name="value">
        /// The associated date html id.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder AssociatedDateHtmlId(string value)
        {
            this.Component.AssociatedDateHtmlId = value;
            return this;
        }

        /// <summary>
        /// The calendar image path.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder CalendarImagePath(string value)
        {
            this.Component.CalendarImagePath = value;
            return this;
        }

        /// <summary>
        /// The tool tip text.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder ExternalLabelText(string value)
        {
            this.Component.ExternalLabelText = value;
            return this;
        }

        /// <summary>
        /// The erase image path.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder EraseImagePath(string value)
        {
            this.Component.EraseImagePath = value;
            return this;
        }

        /// <summary>
        /// The information icon path.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder InformationIconPath(string value)
        {
            this.Component.InformationIconPath = value;
            return this;
        }

        /// <summary>
        /// The conditional annotations.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder ConditionalAnnotations(Dictionary<string, string> value)
        {
            this.Component.ConditionalAnnotations = value;
            return this;
        }

        /// <summary>
        /// The mandatory.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder Mandatory(bool value)
        {
            this.Component.IsMandatory = value;
            return this;
        }

        /// <summary>
        /// The mandatory message.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DateTimeBuilder"/>.
        /// </returns>
        public DateTimeBuilder MandatoryMessage(string value)
        {
            this.Component.MandatoryMessage = value;
            return this;
        }

        /// <summary>
        /// Text of hour hidden span.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DateTimeBuilder AccessHrText(string value)
        {
            this.Component.AccessHrText = value;
            return this;
        }

        /// <summary>
        /// Text of minute text.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DateTimeBuilder AccessMinText(string value)
        {
            this.Component.AccessMinText = value;
            return this;
        }

        /// <summary>
        /// This sets the path of the current date selection image url.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DateTimeBuilder CurrentDateSelectionImageUrl(string value)
        {
            this.Component.CurrentDateSelectionImageUrl = value;
            return this;
        }

        /// <summary>
        /// This sets the path of the current date selection title.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DateTimeBuilder CurrentDateSelectionTitle(string value)
        {
            this.Component.CurrentDateSelectionTitle = value;
            return this;
        }

        /// <summary>
        /// This sets the value to display current date selector.
        /// </summary>
        public DateTimeBuilder DisplayCurrentDateSelector(bool value)
        {
            this.Component.DisplayCurrentDateSelector = value;
            return this;
        }
    }
}
