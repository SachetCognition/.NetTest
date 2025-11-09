// -------------------------------------------------------------------------------------------------
// <copyright file="ActionButtonBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 07/04/2014
//    Author:  Sharma Siddharth (54626) 
//    Description: The Builder class for the ActionButton component
// </summary>
// -------------------------------------------------------------------------------------------------
namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ActionButton
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    /// <summary>
    /// The Builder class for the ActionButton component
    /// </summary>
    public class ActionButtonBuilder : ComponentBuilderBase<ActionButtonComponent, ActionButtonBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionButtonBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="modelMetadata"></param>
        public ActionButtonBuilder(ActionButtonComponent component, ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
        }

        /// <summary>
        /// The method to set CSS class for the component.
        /// </summary>
        /// <param name="css">
        /// CSS class.
        /// </param>
        /// <returns>
        /// The <see cref="ActionButtonBuilder"/>.
        /// </returns>
        public ActionButtonBuilder CssClass(string css)
        {
            Component.CssClass = css;
            return this;
        }

        /// <summary>
        /// The method to set CSS class for ReadOnly mode for the ActionButton.
        /// </summary>
        /// <param name="css">
        /// CSS class for ReadOnly mode.
        /// </param>
        /// <returns>
        /// The <see cref="ActionButtonBuilder"/>.
        /// </returns>
        public ActionButtonBuilder CssClassReadOnly(string css)
        {
            Component.CssClassReadOnly = css;
            return this;
        }

        /// <summary>
        /// CSS class for the span containing the text
        /// </summary>
        /// <param name="css"></param>
        /// <returns></returns>
        public ActionButtonBuilder CssSpan(string css)
        {
            Component.CssSpan = css;
            return this;
        }
        /// <summary>
        /// dialog div Id for confirmation box
        /// </summary>
        /// <param name="divId"></param>
        /// <returns></returns>
        public ActionButtonBuilder DialogBoxId(string divId)
        {
            Component.DialogBoxId = divId;
            return this;
        }

        /// <summary>
        /// The method to set disabled status for ActionButton.
        /// </summary>
        /// <param name="isDisabled">
        /// Disabled status for ActionButton.
        /// </param>
        /// <returns>
        /// The <see cref="ActionButtonBuilder"/>.
        /// </returns>
        public ActionButtonBuilder Disabled(bool isDisabled)
        {
            Component.IsDisabled = isDisabled;
            return this;
        }

        /// <summary>
        /// The method to set click event for component.
        /// </summary>
        /// <param name="value">
        /// Click Event.
        /// </param>
        /// <returns>
        /// The <see cref="ActionButtonBuilder"/>.
        /// </returns>
        public ActionButtonBuilder OnClick(string value)
        {
            Component.OnClick = value;
            return this;
        }

        /// <summary>
        /// The method to set title for component.
        /// </summary>
        /// <param name="value">
        /// Title text for component.
        /// </param>
        /// <returns>
        /// The <see cref="ActionButtonBuilder"/>.
        /// </returns>
        public ActionButtonBuilder Title(string value)
        {
            Component.Title = value;
            return this;
        }

        /// <summary>
        /// The method to set value for component.
        /// </summary>
        /// <param name="val">
        /// Title text for component.
        /// </param>
        /// <returns>
        /// The <see cref="ActionButtonBuilder"/>.
        /// </returns>
        public ActionButtonBuilder Value(string val)
        {
            Component.Value = val;
            return this;
        }

        /// <summary>
        /// The text to be displayed on the button
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ActionButtonBuilder Text(string value)
        {
            Component.Text = value;
            return this;
        }

        /// <summary>
        /// The text for accessibility
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public ActionButtonBuilder AccessText(string text)
        {
            Component.AccessText = text;
            return this;
        }
    }
}
