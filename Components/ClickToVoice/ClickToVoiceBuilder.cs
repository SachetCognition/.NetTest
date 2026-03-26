// -------------------------------------------------------------------------------------------------
// <copyright file="ClickToVoiceBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>    
//    Creation Date: 23/09/2014
//    Author:  Arun Kumar (060644) 
//    Description: The builder class for the ClickToVoice component.
// </summary>
// -------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ClickToVoice
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// The ClickToVoice HTML Builder class
    /// </summary>
    public class ClickToVoiceBuilder : ComponentBuilderBase<ClickToVoiceComponent, ClickToVoiceBuilder>
    {
                /// <summary>
        /// Initializes a new instance of the <see cref="ClickToVoiceBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="modelMetadata"></param>
        public ClickToVoiceBuilder(ClickToVoiceComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }

        /// <summary>
        /// The method to set CSS for the component.
        /// </summary>
        /// <param name="value">
        /// CSS class.
        /// </param>
        /// <returns>
        /// The <see cref="ClickToVoiceBuilder"/>.
        /// </returns>
        public ClickToVoiceBuilder CssAnchorTag(string value)
        {
            Component.CssAnchorTag = value;
            return this;
        }

        /// <summary>
        /// CSS class for the Image tag.
        /// </summary>
        /// <param name="css"></param>
        /// <returns></returns>
        public ClickToVoiceBuilder CssClassImage(string css)
        {
            Component.CssClassImage = css;
            return this;
        }

        /// <summary>
        /// CSS class for the TelephoneNumber Span.
        /// </summary>
        /// <param name="css"></param>
        /// <returns></returns>
        public ClickToVoiceBuilder CssTelephoneNumberSpan(string css)
        {
            Component.CssTelephoneNumberSpan = css;
            return this;
        }

        /// <summary>
        /// Boolean to set the visibility of the TelephoneNumber Span.
        /// </summary>
        /// <param name="css"></param>
        /// <returns></returns>
        public ClickToVoiceBuilder IsNumberSpanVisible(bool css)
        {
            Component.IsNumberSpanVisible = css;
            return this;
        }

        /// <summary>
        /// The method to set HREF for component.
        /// </summary>
        /// <param name="value">
        /// HREF for component.
        /// </param>
        /// <returns>
        /// The <see cref="ClickToVoiceBuilder"/>.
        /// </returns>
        public ClickToVoiceBuilder ActionUrl(string value)
        {
            Component.ActionUrl = value;
            return this;
        }

        /// <summary>
        /// The method to set title for component.
        /// </summary>
        /// <param name="value">
        /// Title text for component.
        /// </param>
        /// <returns>
        /// The <see cref="ClickToVoiceBuilder"/>.
        /// </returns>
        public ClickToVoiceBuilder Title(string value)
        {
            Component.Title = value;
            return this;
        }
        
        /// <summary>
        /// The method to set image url for ClickToVoice Component.
        /// </summary>
        /// <param name="value">
        /// Image Url for ClickToVoice Component.
        /// </param>
        /// <returns>
        /// The <see cref="ClickToVoiceBuilder"/>.
        /// </returns>
        public ClickToVoiceBuilder ImageUrl(string value)
        {
            Component.ImageUrl = value;
            return this;
        }

        /// <summary>
        /// The method to set alternate text for component.
        /// </summary>
        /// <param name="value">
        /// Alternate text for component.
        /// </param>
        /// <returns>
        /// The <see cref="ClickToVoiceBuilder"/>.
        /// </returns>
        public ClickToVoiceBuilder AlternateText(string value)
        {
            Component.AlternateText = value;
            return this;
        }

        /// <summary>
        /// The method to set Telephone Number for component.
        /// </summary>
        /// <param name="value">
        /// Telephone Number text for component.
        /// </param>
        /// <returns>
        /// The <see cref="ClickToVoiceBuilder"/>.
        /// </returns>
        public ClickToVoiceBuilder TelephoneNumber(string value)
        {
            Component.TelephoneNumber = value;
            return this;
        }

        /// <summary>
        /// The method to set TelephoneControlId for component.
        /// </summary>
        /// <param name="value">
        /// TelephoneControlId text for component.
        /// </param>
        /// <returns>
        /// The <see cref="ClickToVoiceBuilder"/>.
        /// </returns>
        public ClickToVoiceBuilder TelephoneControlId(string value)
        {
            Component.TelephoneControlId = value;
            return this;
        }

        /// <summary>
        /// The method to set RootService for component.
        /// </summary>
        /// <param name="value">
        /// RootService text for component.
        /// </param>
        /// <returns>
        /// The <see cref="ClickToVoiceBuilder"/>.
        /// </returns>
        public ClickToVoiceBuilder RootService(string value)
        {
            Component.RootService = value;
            return this;
        }
    }
}
