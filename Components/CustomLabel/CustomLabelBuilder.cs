// -------------------------------------------------------------------------------------------------
// <copyright file="CustomLabelBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 04/04/2014
//    Author:  Sharma Siddharth (54626) 
//    Description: Contains methods to build the CustomLabel component
// </summary>
// -------------------------------------------------------------------------------------------------
namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System;

    /// <summary>
    /// The check box builder.
    /// </summary>
    public class CustomLabelBuilder : ComponentBuilderBase<CustomLabelComponent, CustomLabelBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomLabelBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="modelMetadata"></param>
        public CustomLabelBuilder(CustomLabelComponent component, ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
        }

        /// <summary>
        /// The method to set CSS for the component.
        /// </summary>
        /// <param name="value">
        /// CSS for CustomLabel.
        /// </param>
        /// <returns>
        /// The <see cref="CustomLabelBuilder"/>.
        /// </returns>
        public CustomLabelBuilder IsOnlyForAccess(bool value)
        {
            this.Component.IsOnlyForAccess = value;
            return this;
        }

        /// <summary>
        /// The method to set CSS for the component.
        /// </summary>
        /// <param name="value">
        /// CSS for CustomLabel.
        /// </param>
        /// <returns>
        /// The <see cref="CustomLabelBuilder"/>.
        /// </returns>
        public CustomLabelBuilder CssClassLabel(string value)
        {
            Component.CssClassLabel = value;
            return this;
        }

        /// <summary>
        /// The method to set associatedControlId for the component.
        /// </summary>
        /// <param name="value">
        /// associatedControlId for CustomLabel.
        /// </param>
        /// <returns>
        /// The <see cref="CustomLabelBuilder"/>.
        /// </returns>
        public CustomLabelBuilder AssociatedControlId(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }

            Component.AssociatedControlId = value;
            return this;
        }

        /// <summary>
        /// The method to set Text for the component.
        /// </summary>
        /// <param name="value">
        /// Text for CustomLabel.
        /// </param>
        /// <returns>
        /// The <see cref="CustomLabelBuilder"/>.
        /// </returns>
        public CustomLabelBuilder Text(string value)
        {
            Component.Text = value;
            return this;
        }

        /// <summary>
        /// The method to set SuperscriptText for the component.
        /// </summary>
        /// <param name="value">
        /// SuperscriptText for CustomLabel.
        /// </param>
        /// <returns>
        /// The <see cref="CustomLabelBuilder"/>.
        /// </returns>
        public CustomLabelBuilder SuperScriptText(string value)
        {
            Component.SuperscriptText = value;
            return this;
        }

        /// <summary>
        /// The method to set Superscript CSS Class for the component.
        /// </summary>
        /// <param name="value">
        /// SuperscriptCSSClass for CustomLabel.
        /// </param>
        /// <returns>
        /// The <see cref="CustomLabelBuilder"/>.
        /// </returns>
        public CustomLabelBuilder SuperscriptCssClass(string value)
        {
            Component.SuperscriptCssClass = value;
            return this;
        }

        /// <summary>
        /// The method to set SuperscriptToolTip for the component.
        /// </summary>
        /// <param name="value">
        /// SuperscriptToolTip for CustomLabel.
        /// </param>
        /// <returns>
        /// The <see cref="CustomLabelBuilder"/>.
        /// </returns>
        public CustomLabelBuilder SuperscriptToolTip(string value)
        {
            Component.SuperscriptToolTip = value;
            return this;
        }

        /// <summary>
        /// The method to set DisplayStar for the component.
        /// </summary>
        /// <param name="value">
        /// DisplayStar for CustomLabel.
        /// </param>
        /// <returns>
        /// The <see cref="CustomLabelBuilder"/>.
        /// </returns>
        public CustomLabelBuilder DisplayStar(bool value)
        {
            Component.DisplayStar = value;
            return this;
        }

        /// <summary>
        /// The method to set DisplayColon for the component.
        /// </summary>
        /// <param name="value">
        /// DisplayColon for CustomLabel.
        /// </param>
        /// <returns>
        /// The <see cref="CustomLabelBuilder"/>.
        /// </returns>
        public CustomLabelBuilder DisplayColon(bool value)
        {
            Component.DisplayColon = value;
            return this;
        }

        /// <summary>
        /// The method to set IsHtmlEncode for the component.
        /// </summary>
        /// <param name="value">
        /// IsHtmlEncode for CustomLabel.
        /// </param>
        /// <returns>
        /// The <see cref="CustomLabelBuilder"/>.
        /// </returns>
        public CustomLabelBuilder IsHtmlEncode(bool value)
        {
            Component.IsHtmlEncode = value;
            return this;
        }

        /// <summary>
        /// The method to set AccessText for the component.
        /// </summary>
        /// <param name="value">
        /// AccessText for CustomLabel.
        /// </param>
        /// <returns>
        /// The <see cref="CustomLabelBuilder"/>.
        /// </returns>
        public CustomLabelBuilder AccessText(string value)
        {
            Component.AccessText = value;
            return this;
        }

        /// <summary>
        /// </summary>
        public CustomLabelBuilder HtmlAttributes(object htmlAttributes)
        {
            if (htmlAttributes != null)
            {
                var properties = htmlAttributes.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    Component.HtmlAttributes[prop.Name] = prop.GetValue(htmlAttributes);
                }
            }
            return this;
        }
    }
}
