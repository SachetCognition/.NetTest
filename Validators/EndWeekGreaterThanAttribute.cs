// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndWeekGreaterThanAttribute.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 01/07/2014
//   Author:  Sharma Siddharth (54626)
//   Description: This defines end week greater than other week attribute for validation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;

    /// <summary>
    /// The date greater than attribute.
    /// This is an example of a custom validator implementation
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class EndWeekGreaterThanAttribute : ValidationAttribute
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
        /// <param name="otherPropertyName">
        /// The other property name.
        /// </param>
        /// <param name="otherPropertyHtmlId">
        /// The other Property Html Id.
        /// </param>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        public EndWeekGreaterThanAttribute(string otherPropertyName, string otherPropertyHtmlId, string errorMessage)
            : base(errorMessage)
        {
            this.otherPropertyName = otherPropertyName;
            this.otherPropertyHtmlId = otherPropertyHtmlId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EndWeekGreaterThanAttribute"/> class.
        /// </summary>
        /// <param name="otherPropertyName">
        /// The other property name.
        /// </param>
        /// <param name="otherPropertyHtmlId">
        /// The other property html id.
        /// </param>
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
                        validationResult = new ValidationResult(this.ErrorMessage ?? "End week must be greater than start week");
                    }
                }
            }

            return validationResult;
        }
    }
}
