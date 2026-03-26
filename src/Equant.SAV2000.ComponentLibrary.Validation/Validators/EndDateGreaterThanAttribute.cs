// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndDateGreaterThanAttribute.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 26/05/2014
//   Author:  Sharma Siddharth (54626)
//   Description: The class which defines End Date Greater than attribute.
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

    /// <summary>
    /// The end date greater than attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class EndDateGreaterThanAttribute : ValidationAttribute, IClientModelValidator
    {
        /// <summary>
        /// The other property name.
        /// </summary>
        private readonly string otherPropertyName;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndDateGreaterThanAttribute"/> class. 
        /// </summary>
        public EndDateGreaterThanAttribute(string otherPropertyName, string errorMessage)
             : base(errorMessage)
        {
            this.otherPropertyName = otherPropertyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EndDateGreaterThanAttribute"/> class.
        /// </summary>
        public EndDateGreaterThanAttribute(string otherPropertyName)
            : this(otherPropertyName, string.Empty)
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
        /// Adds client-side validation attributes for unobtrusive validation.
        /// </summary>
        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-enddategreaterthan", ErrorMessageString);
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
