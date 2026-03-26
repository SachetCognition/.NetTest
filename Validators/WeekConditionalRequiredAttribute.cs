// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeekConditionalRequiredAttribute.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 30/06/2014
//   Author:  Sharma Siddharth (54626)
//   Description: This defines the WeekConditionalRequiredAttribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;
    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    /// <summary>
    /// The date greater than attribute.
    /// This is an example of a custom validator implementation
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
        /// <param name="otherPropertyName">
        /// The other property name.
        /// </param>
        /// <param name="otherPropertyHtmlId">
        /// The other Property Html Id.
        /// </param>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        public WeekConditionalRequiredAttribute(string otherPropertyName, string otherPropertyHtmlId, string errorMessage)
            : base(errorMessage)
        {
            this.otherPropertyName = otherPropertyName;
            this.otherPropertyHtmlId = otherPropertyHtmlId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeekConditionalRequiredAttribute"/> class. 
        /// </summary>
        /// <param name="otherPropertyName">
        /// The other property name.
        /// </param>
        /// <param name="otherPropertyHtmlId">
        /// The other Property Html Id.
        /// </param>
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
            if (!context.Attributes.ContainsKey("data-val-weekconditionalrequired"))
            {
                context.Attributes.Add("data-val-weekconditionalrequired", this.ErrorMessageString);
            }
            if (!context.Attributes.ContainsKey("data-val-weekconditionalrequired-otherweekid"))
            {
                context.Attributes.Add("data-val-weekconditionalrequired-otherweekid", this.otherPropertyHtmlId);
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
    }
}