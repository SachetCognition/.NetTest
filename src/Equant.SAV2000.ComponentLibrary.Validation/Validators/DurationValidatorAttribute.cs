// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DurationValidatorAttribute.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 29/07/2014
//   Author:  Sharma Nipun
//   Description: This class validates the input duration against the provided timestamp
//   Migrated to .NET 8+ / ASP.NET Core
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateDurationControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;

    /// <summary>
    /// The duration validator attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DurationValidatorAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string firstPropertyHtmlId;
        private readonly string firstPropertyName;
        private readonly string secondPropertyHtmlId;
        private readonly string secondPropertyName;
        private readonly string thirdPropertyHtmlId;

        /// <summary>
        /// Initializes a new instance of the <see cref="DurationValidatorAttribute"/> class.
        /// </summary>
        public DurationValidatorAttribute(
            string firstPropertyName,
            string secondPropertyName,
            string firstPropertyHtmlId,
            string secondPropertyHtmlId,
            string thirdPropertyHtmlId,
            string errorMessage)
            : base(errorMessage)
        {
            this.firstPropertyName = firstPropertyName;
            this.secondPropertyName = secondPropertyName;
            this.firstPropertyHtmlId = firstPropertyHtmlId;
            this.secondPropertyHtmlId = secondPropertyHtmlId;
            this.thirdPropertyHtmlId = thirdPropertyHtmlId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DurationValidatorAttribute"/> class.
        /// </summary>
        public DurationValidatorAttribute(
            string firstPropertyName, string secondPropertyName, string firstPropertyHtmlId, string secondPropertyHtmlId, string thirdPropertyHtmlId)
            : this(firstPropertyName, secondPropertyName, firstPropertyHtmlId, secondPropertyHtmlId, thirdPropertyHtmlId, string.Empty)
        {
        }

        public string FirstPropertyHtmlId
        {
            get { return this.firstPropertyHtmlId; }
        }

        public string FirstPropertyName
        {
            get { return this.firstPropertyName; }
        }

        public string SecondPropertyHtmlId
        {
            get { return this.secondPropertyHtmlId; }
        }

        public string SecondPropertyName
        {
            get { return this.secondPropertyName; }
        }

        public string ThirdPropertyHtmlId
        {
            get { return this.thirdPropertyHtmlId; }
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
            MergeAttribute(context.Attributes, "data-val-durationvalidator", ErrorMessageString);
            MergeAttribute(context.Attributes, "data-val-durationvalidator-startdateid", this.firstPropertyHtmlId);
            MergeAttribute(context.Attributes, "data-val-durationvalidator-enddateid", this.secondPropertyHtmlId);
            MergeAttribute(context.Attributes, "data-val-durationvalidator-datedurationid", this.thirdPropertyHtmlId);
        }

        /// <summary>
        /// The is valid.
        /// </summary>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validationResult = ValidationResult.Success;
            if (validationContext != null)
            {
                var inputduration = value as DateDuration;
                var firstPropertyInfo = validationContext.ObjectType.GetProperty(this.FirstPropertyName);
                var secondPropertyInfo = validationContext.ObjectType.GetProperty(this.SecondPropertyName);
                var startDate = (DateTimeWithFormat)firstPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                var endDate = (DateTimeWithFormat)secondPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                var timediffernce = Convert.ToDateTime(endDate.Date, CultureInfo.InvariantCulture) - Convert.ToDateTime(startDate.Date, CultureInfo.InvariantCulture);
                var datetimeduration = new TimeSpan(
                    timediffernce.Days, timediffernce.Hours, timediffernce.Minutes, timediffernce.Seconds, timediffernce.Milliseconds);

                if (inputduration != null)
                {
                    var duration = new TimeSpan(
                        0,
                        Convert.ToInt32(inputduration.Hour, CultureInfo.InvariantCulture),
                        Convert.ToInt32(inputduration.Minute, CultureInfo.InvariantCulture),
                        Convert.ToInt32(inputduration.Second, CultureInfo.InvariantCulture),
                        Convert.ToInt32(inputduration.MiliSecond, CultureInfo.InvariantCulture));

                    validationResult = datetimeduration.TotalMilliseconds >= duration.TotalMilliseconds
                                           ? ValidationResult.Success
                                           : new ValidationResult("Invalid Duration");
                }
                else
                {
                    validationResult = new ValidationResult("Duration cannot be black.Please specify the duration");
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
