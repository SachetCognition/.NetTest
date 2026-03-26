// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndWeekGreaterThanAttribute.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 01/07/2014
//   Author:  Sharma Siddharth (54626)
//   Description: This defines end week greater than other week attribute for validation.
//   Migrated to .NET 8+ / ASP.NET Core
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;

    /// <summary>
    /// The end week greater than attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class EndWeekGreaterThanAttribute : ValidationAttribute, IClientModelValidator
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
        /// Initializes a new instance of the <see cref="EndWeekGreaterThanAttribute"/> class. 
        /// </summary>
        public EndWeekGreaterThanAttribute(string otherPropertyName, string otherPropertyHtmlId, string errorMessage)
            : base(errorMessage)
        {
            this.otherPropertyName = otherPropertyName;
            this.otherPropertyHtmlId = otherPropertyHtmlId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EndWeekGreaterThanAttribute"/> class.
        /// </summary>
        public EndWeekGreaterThanAttribute(string otherPropertyName, string otherPropertyHtmlId)
            : this(otherPropertyName, otherPropertyHtmlId, string.Empty)
        {
        }

        /// <summary>
        /// Gets other property name.
        /// </summary>
        public string OtherPropertyName
        {
            get
            {
                return this.otherPropertyName;
            }
        }

        /// <summary>
        /// Gets other property html id.
        /// </summary>
        public string OtherPropertyHtmlId
        {
            get
            {
                return this.otherPropertyHtmlId;
            }
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
            MergeAttribute(context.Attributes, "data-val-endweekgreaterthan", ErrorMessageString);
            MergeAttribute(context.Attributes, "data-val-endweekgreaterthan-lesserweekid", this.otherPropertyHtmlId);
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

            var endDate = value as WeekYearWithFormat;
            if (endDate == null)
            {
                validationResult = new ValidationResult("An error occurred while validating the property. Property is not of type WeekYearWithFormat");
                return validationResult;
            }

            if (validationContext == null)
            {
                throw new ArgumentException("validation context cannot be null");
            }

            var otherPropertyInfo = validationContext.ObjectType.GetProperty(this.otherPropertyName);

            if (otherPropertyInfo.PropertyType != typeof(WeekYearWithFormat))
            {
                validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type WeekYearWithFormat");
                return validationResult;
            }

            var lesserDate = (WeekYearWithFormat)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (otherPropertyInfo.PropertyType == typeof(WeekYearWithFormat))
            {
                if (lesserDate == null)
                {
                    return validationResult;
                }

                if (endDate.Date != null && lesserDate.Date != null)
                {
                    if (endDate.Date.Value.CompareTo(lesserDate.Date.Value) < 0)
                    {
                        validationResult = new ValidationResult(this.ErrorMessageString);
                    }
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
