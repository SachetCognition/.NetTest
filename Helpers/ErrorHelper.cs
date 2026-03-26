// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorHelper.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 04/01/2015
//   Author:  Gupta Rupesh Kumar
//   Legacy mapping:
//   Description:
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System;
using System.Collections.Generic;


namespace Equant.SAV2000.ComponentLibrary.MVC.Helpers
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// The error helper.
    /// </summary>
    public static class ErrorHelper
    {
        /// <summary>
        /// The create error component.
        /// </summary>
        /// <param name="componentBase">
        /// The component base.
        /// </param>
        /// <returns>
        /// The <see cref="ErrorComponents"/>.
        /// </returns>
        public static ErrorComponents CreateErrorComponent(ComponentBase componentBase)
        {
            return componentBase == null ? null : CreateErrorComponent(componentBase, null, componentBase.ValidationString.ToString());
        }

        /// <summary>
        /// The create error component.
        /// </summary>
        /// <param name="componentBase">
        ///     The component base.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="accessText">Text to show field name</param>
        /// <returns>
        /// The <see cref="ErrorComponents"/>.
        /// </returns>
        public static ErrorComponents CreateErrorComponent(ComponentBase componentBase, string accessText)
        {
            if (componentBase == null)
            {
                throw new ArgumentNullException("componentBase");
            }

            var errorComponent = new ErrorComponents(componentBase.HtmlHelper);
            var errorMessag = new List<ErrorMessageModel> { new ErrorMessageModel { ErrorHtml = componentBase.ValidationString.ToString() } };
            new ErrorBuilder(errorComponent, componentBase.ModelMetadata).ErrorCollection(errorMessag).Id(componentBase.Id + "_Error").AccessibleText(accessText).DivCssClass("error-input");
            return errorComponent;
        }

        public static ErrorComponents CreateErrorComponent(ComponentBase componentBase, string accessText, string errorHtml)
        {
            if (componentBase == null)
            {
                throw new ArgumentNullException("componentBase");
            }

            var errorComponent = new ErrorComponents(componentBase.HtmlHelper);
            var errorMessag = new List<ErrorMessageModel> { new ErrorMessageModel { ErrorHtml = errorHtml } };
            new ErrorBuilder(errorComponent, componentBase.ModelMetadata).ErrorCollection(errorMessag).Id(componentBase.Id + "_Error").AccessibleText(accessText).DivCssClass("error-input");
            return errorComponent;
        }


    }
}
