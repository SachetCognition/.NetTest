// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateConditionalRequiredAttribute.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 26/05/2014
//   Author:  Sharma Siddharth (54626)
//   Description: This defines date conditionally required attribute
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
    /// The date conditional required attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DateConditionalRequiredAttribute : ValidationAttribute, IClientModelValidator
    {
        /// <summary>
        /// The other property name.
        /// </summary>
        private readonly string otherPropertyName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateConditionalRequiredAttribute"/> class. 
        /// </summary>
        public DateConditionalRequiredAttribute(string otherPropertyName, string errorMessage)
            : base(errorMessage)
        {
            this.otherPropertyName = otherPropertyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateConditionalRequiredAttribute"/> class. 
        /// </summary>
        public DateConditionalRequiredAttribute(string otherPropertyName)
            : this(otherPropertyName, string.Empty)
        {
        }

        /// <summary>
        /// Gets the other property name.
        /// </summary>
        public string OtherPropertyName
        {
            get
            {
                return this.otherPropertyName;
            }
        }

        /// <summary>
        /// Gets or sets the date type property name.
        /// </summary>
        public string DateTypePropertyName { get; set; }

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
            MergeAttribute(context.Attributes, "data-val-dateconditionalrequired", ErrorMessageString);
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

            var thisDate = value as DateTimeWithFormat;
            if (thisDate == null)
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

            var otherPropertyInfo = validationContext.ObjectType.GetProperty(this.otherPropertyName);

            if (otherPropertyInfo.PropertyType != typeof(DateTimeWithFormat))
            {
                validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type DateTimeWithFormat");
                return validationResult;
            }

            var otherDate = (DateTimeWithFormat)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (otherPropertyInfo.PropertyType == typeof(DateTimeWithFormat))
            {
                if (otherDate == null)
                {
                    return validationResult;
                }

                if (otherDate.Date != null && string.IsNullOrEmpty(thisDate.DateText) && string.IsNullOrEmpty(thisDate.HourValue) &&
                    string.IsNullOrEmpty(thisDate.MinuteValue))
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
