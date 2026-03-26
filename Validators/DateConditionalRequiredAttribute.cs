// -------------------------------------------------------------------------------------------------------------------
// <copyright file="DateConditionalRequiredAttribute.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 26/05/2014
//   Author:  Sharma Siddharth (54626)
//   Description: This defines date contionally required attribute
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    /// <summary>
    /// The date greater than attribute.
    /// This is an example of a custom validator implementation
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
        /// <param name="otherPropertyName">
        /// The other property name.
        /// </param>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        public DateConditionalRequiredAttribute(string otherPropertyName,  string errorMessage)
            : base(errorMessage)
        {
            this.otherPropertyName = otherPropertyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateConditionalRequiredAttribute"/> class. 
        /// </summary>
        /// <param name="otherPropertyName">
        /// The other property name.
        /// </param>
        /// <param name="otherPropertyHtmlId">
        /// The other Property Html Id.
        /// </param>
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
        /// The get client validation rules.
        /// </summary>
        /// <param name="metadata">
        // The metadata.
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
            if (!context.Attributes.ContainsKey("data-val-dateconditionalrequired"))
            {
                context.Attributes.Add("data-val-dateconditionalrequired", this.ErrorMessageString);
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
    }
}