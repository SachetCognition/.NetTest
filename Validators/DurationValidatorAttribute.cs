// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DurationValidatorAttribute.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 29/07/2014
//   Author:  Sharma Nipun
//   Description:This class validates the input duration against the provided timestamp
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateDurationControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;

    /// <summary>
    /// The date greater than attribute.
    /// This is an example of a custom validator implementation
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DurationValidatorAttribute : ValidationAttribute
    {
        #region Fields

        /// <summary>
        /// The other property html id.
        /// </summary>
        private readonly string firstPropertyHtmlId;

        /// <summary>
        /// The first property name.
        /// </summary>
        private readonly string firstPropertyName;

        /// <summary>
        /// The second property html id.
        /// </summary>
        private readonly string secondPropertyHtmlId;

        /// <summary>
        /// The second property name.
        /// </summary>
        private readonly string secondPropertyName;

        /// <summary>
        /// The third property html id.
        /// </summary>
        private readonly string thirdPropertyHtmlId;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DurationValidatorAttribute"/> class. 
        /// Initializes a new instance of the <see cref="Equant.SAV2000.ComponentLibrary.MVC.Validators.EndDateGreaterThanAttribute"/> class. 
        /// </summary>
        /// <param name="firstPropertyName">
        /// The first property name.
        /// </param>
        /// <param name="secondPropertyName">
        /// 
        /// The second property name.
        /// </param>
        /// <param name="firstPropertyHtmlId">
        /// 
        /// The first property Id.
        /// </param>
        /// <param name="secondPropertyHtmlId">
        /// 
        /// The second property Id.
        /// </param>
        /// <param name="thirdPropertyHtmlId">
        /// 
        /// The third property Id.
        /// </param>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
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
        /// Initializes a new instance of the <see cref="Equant.SAV2000.ComponentLibrary.MVC.Validators.EndDateGreaterThanAttribute"/> class. 
        /// </summary>
        /// <param name="firstPropertyName">
        /// The first property name.
        /// </param>
        /// <param name="secondPropertyName">
        /// 
        /// The second property name.
        /// </param>
        /// <param name="firstPropertyHtmlId">
        /// 
        /// The first property Id.
        /// </param>
        /// <param name="secondPropertyHtmlId">
        /// 
        /// The second property Id.
        /// </param>
        /// <param name="thirdPropertyHtmlId">
        /// 
        /// The third property Id.
        /// </param>
        public DurationValidatorAttribute(
            string firstPropertyName, string secondPropertyName, string firstPropertyHtmlId, string secondPropertyHtmlId, string thirdPropertyHtmlId)
            : this(firstPropertyName, secondPropertyName, firstPropertyHtmlId, secondPropertyHtmlId, thirdPropertyHtmlId, string.Empty)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets first property html id.
        /// </summary>
        public string FirstPropertyHtmlId
        {
            get
            {
                return this.firstPropertyHtmlId;
            }
        }

        /// <summary>
        /// Gets other property name.
        /// </summary>
        public string FirstPropertyName
        {
            get
            {
                return this.firstPropertyName;
            }
        }

        /// <summary>
        /// Gets other property html id.
        /// </summary>
        public string SecondPropertyHtmlId
        {
            get
            {
                return this.secondPropertyHtmlId;
            }
        }

        /// <summary>
        /// Gets second property name.
        /// </summary>
        public string SecondPropertyName
        {
            get
            {
                return this.secondPropertyName;
            }
        }

        /// <summary>
        /// Gets third property html id.
        /// </summary>
        public string ThirdPropertyHtmlId
        {
            get
            {
                return this.thirdPropertyHtmlId;
            }
        }

        #endregion

        #region Public Methods and Operators

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

        #endregion

        #region Methods

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

        #endregion
    }
}