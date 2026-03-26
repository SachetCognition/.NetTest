// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateRequiredAttribute.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 25/05/2014
//   Author:  Sharma Siddharth (54626)
//   Description: This implements custom required attribute for DateTimeWithFormat class
//   Migrated to .NET 8+ / ASP.NET Core
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;

    /// <summary>
    /// The date required attribute.
    /// This is date required attribute class
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DateRequiredAttribute : ValidationAttribute, IClientModelValidator
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
        /// Adds client-side validation attributes for unobtrusive validation.
        /// Replaces IClientValidatable.GetClientValidationRules from .NET Framework.
        /// </summary>
        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-daterequired", ErrorMessageString);
        }

        /// <summary>
        /// The is valid.
        /// </summary>
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

        private static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }
    }
}
