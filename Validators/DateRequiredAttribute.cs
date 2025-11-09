// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateRequiredAttribute.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 25/05/2014
//   Author:  Sharma Siddharth (54626)
//   Description: This implements custom required attribute for DateTimeWithFormat class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;

    /// <summary>
    /// The date required attribute.
    /// This is date required attribute class
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DateRequiredAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateRequiredAttribute"/> class.
        /// </summary>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        public DateRequiredAttribute(string errorMessage) : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateRequiredAttribute"/> class.
        /// </summary>
        public DateRequiredAttribute()
        {
        }

        /// <summary>
        /// Gets or sets the date type property name.
        /// </summary>
        public string DateTypePropertyName { get; set; }

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
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var dateRequiredRule = new ModelClientValidationRule { ErrorMessage = this.ErrorMessageString, ValidationType = "daterequired" };
            yield return dateRequiredRule;
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

            var date = value as DateTimeWithFormat;
            
            if (date == null)
            {
                validationResult = new ValidationResult("An error occurred while validating the property. Property is not of type DateTimeWithFormat");
                return validationResult;
            }

            if (validationContext == null)
            {
                throw new ArgumentException("validation context cannot be null");
            }

            if (!string.IsNullOrEmpty(this.DateTypePropertyName))
            {
                var dateTypePropertyInfo = validationContext.ObjectType.GetProperty(this.DateTypePropertyName);
                var dateTypeSelected = (string)dateTypePropertyInfo.GetValue(validationContext.ObjectInstance, null);
                if (!DateComponentHelper.GetDateTypesDouble().Contains(dateTypeSelected))
                {
                    return validationResult;
                }
            }

            if (string.IsNullOrEmpty(date.DateText) && string.IsNullOrEmpty(date.HourValue) && string.IsNullOrEmpty(date.MinuteValue))
            {
                validationResult = new ValidationResult(this.ErrorMessageString);
            }

            return validationResult;
        }
    }
}
