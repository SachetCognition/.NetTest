// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeekConditionalRequiredAttribute.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 30/06/2014
//   Author:  Sharma Siddharth (54626)
//   Description: This defines the WeekConditionalRequiredAttribute.
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

    using Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;
    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;

    /// <summary>
    /// The week conditional required attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class WeekConditionalRequiredAttribute : ValidationAttribute, IClientModelValidator
    {
        /// <summary>
        /// The other property name.
        /// </summary>
        private readonly string otherPropertyName;

        /// <summary>
        /// The other property html id.
        /// </summary>
        private readonly string otherPropertyHtmlId;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeekConditionalRequiredAttribute"/> class. 
        /// </summary>
        public WeekConditionalRequiredAttribute(string otherPropertyName, string otherPropertyHtmlId, string errorMessage)
            : base(errorMessage)
        {
            this.otherPropertyName = otherPropertyName;
            this.otherPropertyHtmlId = otherPropertyHtmlId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeekConditionalRequiredAttribute"/> class. 
        /// </summary>
        public WeekConditionalRequiredAttribute(string otherPropertyName, string otherPropertyHtmlId)
            : this(otherPropertyName, otherPropertyHtmlId, string.Empty)
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
        /// Gets the other property html id.
        /// </summary>
        public string OtherPropertyHtmlId
        {
            get
            {
                return this.otherPropertyHtmlId;
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
            MergeAttribute(context.Attributes, "data-val-weekconditionalrequired", ErrorMessageString);
            MergeAttribute(context.Attributes, "data-val-weekconditionalrequired-otherweekid", this.otherPropertyHtmlId);
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

            var thisWeek = value as WeekYearWithFormat;
            if (thisWeek == null)
            {
                validationResult = new ValidationResult("An error occurred while validating the property. Property is not of type WeekYearWithFormat");
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
                if (!DateComponentHelper.WeekDateTypesDouble().Contains(dateTypeSelected))
                {
                    return validationResult;
                }
            }

            var otherPropertyInfo = validationContext.ObjectType.GetProperty(this.otherPropertyName);

            if (otherPropertyInfo.PropertyType != typeof(WeekYearWithFormat))
            {
                validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type WeekYearWithFormat");
                return validationResult;
            }

            var otherWeek = (WeekYearWithFormat)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (otherPropertyInfo.PropertyType == typeof(WeekYearWithFormat))
            {
                if (otherWeek == null)
                {
                    return validationResult;
                }

                if (otherWeek.Date != null && string.IsNullOrEmpty(thisWeek.WeekText) && string.IsNullOrEmpty(thisWeek.YearText))
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
