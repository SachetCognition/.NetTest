// -------------------------------------------------------------------------------------------------
// <copyright file="CheckBoxBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 13/03/2014
//    Author:  Sharma Siddharth (54626) 
//    Description: Contains methods to build the checkbox component
// </summary>
// -------------------------------------------------------------------------------------------------
namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBox
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// The check box builder.
    /// </summary>
    public class CheckBoxBuilder : ComponentBuilderBase<CheckBoxComponent, CheckBoxBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBoxBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="modelMetadata"></param>
        public CheckBoxBuilder(CheckBoxComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }

        /// <summary>
        /// The method to set checked value for the component.
        /// </summary>
        /// <param name="value">
        /// Checked Status for checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxBuilder"/>.
        /// </returns>
        public CheckBoxBuilder IsChecked(bool value)
        {
            Component.IsChecked = value;
            return this;
        }

        /// <summary>
        /// The method to set CSS Class for the component.
        /// </summary>
        /// <param name="value">
        /// CSS for checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxBuilder"/>.
        /// </returns>
        public CheckBoxBuilder CssClass(string value)
        {
            Component.CssClass = value;
            return this;
        }

        /// <summary>
        /// The method to set CSS Class for the component when it is disabled.
        /// </summary>
        /// <param name="value">
        /// CSS for checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxBuilder"/>.
        /// </returns>
        public CheckBoxBuilder CssClassDisabled(string value)
        {
            Component.CssClassDisabled = value;
            return this;
        }

        /// <summary>
        /// The method to set click event for checkbox.
        /// </summary>
        /// <param name="value">
        /// Click Event for checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxBuilder"/>.
        /// </returns>
        public CheckBoxBuilder OnClick(string value)
        {
            Component.OnClick = value;
            return this;
        }

        /// <summary>
        /// The method to set change event for checkbox.
        /// </summary>
        /// <param name="value">
        /// Change Event for checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxBuilder"/>.
        /// </returns>
        public CheckBoxBuilder OnChange(string value)
        {
            Component.OnChange = value;
            return this;
        }

        /// <summary>
        /// The method to set disabled status for checkbox.
        /// </summary>
        /// <param name="value">
        /// Disabled status for checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxBuilder"/>.
        /// </returns>
        public CheckBoxBuilder Disabled(bool value)
        {
            Component.IsDisabled = value;
            return this;
        }

        /// <summary>
        /// The method to set title for checkbox.
        /// </summary>
        /// <param name="value">
        /// Title text for checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxBuilder"/>.
        /// </returns>
        public CheckBoxBuilder Title(string value)
        {
            Component.Title = value;
            return this;
        }
    }
}
