// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LessThanCurrentDateAttribute.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 25/05/2014
//   Author:  Sharma Siddharth (54626)
//   Description: This defines the Date Less Than Current Date Attribute Class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    /// <summary>
    /// The date required attribute.
    /// This is date required attribute class
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class LessThanCurrentDateAttribute : ValidationAttribute, IClientModelValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LessThanCurrentDateAttribute"/> class.
        /// </summary>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        public LessThanCurrentDateAttribute(string errorMessage) : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LessThanCurrentDateAttribute"/> class.
        /// </summary>
        public LessThanCurrentDateAttribute()
        {
        }

        /// <summary>
        /// The get client validation rules.
        /// </summary>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public void AddValidation(ClientModelValidationContext context)
        {
            if (!context.Attributes.ContainsKey("data-val"))
            {
                context.Attributes.Add("data-val", "true");
            }
            if (!context.Attributes.ContainsKey("data-val-lessthancurrentdate"))
            {
                context.Attributes.Add("data-val-lessthancurrentdate", this.ErrorMessageString);
            }
        }

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="validationContext">
        /// The validation context.
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validationResult = ValidationResult.Success;
            if (value == null)
            {
                return validationResult;
            }

            var dateTimeValue = value as DateTimeWithFormat;
            
            if (dateTimeValue == null)
            {
                validationResult = new ValidationResult("An error occurred while validating the property. Property is not of type DateTimeWithFormat");
                return validationResult;
            }

            if (dateTimeValue.Date != null)
            {
                var currentDate = DateComponentHelper.GetCurrentDate(dateTimeValue);
                if (dateTimeValue.Date.Value < currentDate)
                {
                    validationResult = new ValidationResult(this.ErrorMessageString);
                }
            }

            return validationResult;
        }
    }
}