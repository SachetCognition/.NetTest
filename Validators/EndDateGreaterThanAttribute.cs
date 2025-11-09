// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndDateGreaterThanAttribute.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 26/05/2014
//   Author:  Sharma Siddharth (54626)
//   Description: The class which defines End Date Greater than attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;

    /// <summary>
    /// The date greater than attribute.
    /// This is an example of a custom validator implementation
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class EndDateGreaterThanAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// The other property name.
        /// </summary>
        private readonly string otherPropertyName;

     
        /// <summary>
        /// Initializes a new instance of the <see cref="EndDateGreaterThanAttribute"/> class. 
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
        public EndDateGreaterThanAttribute(string otherPropertyName,  string errorMessage)
             : base(errorMessage)
        {
            this.otherPropertyName = otherPropertyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EndDateGreaterThanAttribute"/> class.
        /// </summary>
        /// <param name="otherPropertyName">
        /// The other property name.
        /// </param>
        /// <param name="otherPropertyHtmlId">
        /// The other property html id.
        /// </param>
        public EndDateGreaterThanAttribute(string otherPropertyName)
            : this(otherPropertyName,  string.Empty)
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
            var dateGreaterThanRule = new ModelClientValidationRule { ErrorMessage = this.ErrorMessageString, ValidationType = "enddategreaterthan" };
            yield return dateGreaterThanRule;
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

            var endDate = value as DateTimeWithFormat;
            if (endDate == null)
            {
                validationResult = new ValidationResult("An error occurred while validating the property. Property is not of type DateTimeWithFormat");
                return validationResult;
            }

            if (validationContext == null)
            {
                throw new ArgumentException("validation context cannot be null");
            }

            var otherPropertyInfo = validationContext.ObjectType.GetProperty(this.otherPropertyName);

            if (otherPropertyInfo.PropertyType != typeof(DateTimeWithFormat))
            {
                validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type DateTimeWithFormat");
                return validationResult;
            }

            var lesserDate = (DateTimeWithFormat)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (otherPropertyInfo.PropertyType == typeof(DateTimeWithFormat))
            {
                if (lesserDate == null)
                {
                    return validationResult;
                }

                if (endDate.Date != null && lesserDate.Date != null)
                {
                    if (endDate.Date.Value.CompareTo(lesserDate.Date.Value) < 1)
                    {
                        validationResult = new ValidationResult(this.ErrorMessageString);
                    }
                }
            }

            return validationResult;
        }
    }
}