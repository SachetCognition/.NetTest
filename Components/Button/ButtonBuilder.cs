// -------------------------------------------------------------------------------------------------
// <copyright file="ButtonBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 07/04/2014
//    Author:  Sharma Siddharth (54626) 
//    Description: The Builder class for the Button component
// </summary>
// -------------------------------------------------------------------------------------------------
namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Button
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// The Builder class for the Button component
    /// </summary>
    public class ButtonBuilder : ComponentBuilderBase<ButtonComponent, ButtonBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="modelMetadata"></param>
        public ButtonBuilder(ButtonComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }

        /// <summary>
        /// The method to set CSS class for the component.
        /// </summary>
        /// <param name="value">
        /// CSS class.
        /// </param>
        /// <returns>
        /// The <see cref="ButtonBuilder"/>.
        /// </returns>
        public ButtonBuilder CssClass(string value)
        {
            Component.CssClass = value;
            return this;
        }

        /// <summary>
        /// The method to set CSS class for ReadOnly mode for the button.
        /// </summary>
        /// <param name="value">
        /// CSS class for ReadOnly mode.
        /// </param>
        /// <returns>
        /// The <see cref="ButtonBuilder"/>.
        /// </returns>
        public ButtonBuilder CssClassReadOnly(string value)
        {
            Component.CssClassReadOnly = value;
            return this;
        }

        /// <summary>
        /// The dialog DIV id.
        /// </summary>
        /// <param name="divId">
        /// The div id.
        /// </param>
        /// <returns>
        /// The <see cref="ButtonBuilder"/>.
        /// </returns>
        public ButtonBuilder DialogDivId(string divId)
        {
            Component.DialogDivId = divId;
            return this;
        }

        /// <summary>
        /// The method to set disabled status for button.
        /// </summary>
        /// <param name="value">
        /// Disabled status for button.
        /// </param>
        /// <returns>
        /// The <see cref="ButtonBuilder"/>.
        /// </returns>
        public ButtonBuilder Disabled(bool value)
        {
            Component.IsDisabled = value;
            return this;
        }

        /// <summary>
        /// The method to set click event for component.
        /// </summary>
        /// <param name="value">
        /// Click Event.
        /// </param>
        /// <returns>
        /// The <see cref="ButtonBuilder"/>.
        /// </returns>
        public ButtonBuilder OnClick(string value)
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
        /// The <see cref="ButtonBuilder"/>.
        /// </returns>
        public ButtonBuilder Title(string value)
        {
            Component.Title = value;
            return this;
        }

        /// <summary>
        /// The method to set value for component.
        /// </summary>
        /// <param name="buttonValue">
        /// Title text for component.
        /// </param>
        /// <returns>
        /// The <see cref="ButtonBuilder"/>.
        /// </returns>
        public ButtonBuilder Value(string buttonValue)
        {
            Component.Value = buttonValue;
            return this;
        }
    }
}
