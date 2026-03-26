// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LessThanCurrentDateAttribute.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 25/05/2014
//   Author:  Sharma Siddharth (54626)
//   Description: This defines the Date Less Than Current Date Attribute Class
//   Migrated to .NET 8+ / ASP.NET Core
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;

    /// <summary>
    /// The less than current date attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class LessThanCurrentDateAttribute : ValidationAttribute, IClientModelValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LessThanCurrentDateAttribute"/> class.
        /// </summary>
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
        /// Adds client-side validation attributes for unobtrusive validation.
        /// </summary>
        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-lessthancurrentdate", ErrorMessageString);
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
