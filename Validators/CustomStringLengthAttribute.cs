using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    using System.Globalization;

    /// <summary>
    /// Specifies the minimum and maximum length of characters that are allowed in a data field
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class CustomStringLengthAttribute : StringLengthAttribute, IClientModelValidator
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomStringLengthAttribute"/> class. 
        /// </summary>
        /// <param name="maximumLength">The other property name.</param>
        public CustomStringLengthAttribute(int maximumLength)
            : base(maximumLength)
        {
        }
      
        /// <summary>
        /// Determines whether a specified object is valid.
        /// </summary>
        /// <param name="value">object</param>
        /// <returns>value</returns>
        public override bool IsValid(object value)
        {

            return base.IsValid(Convert.ToString(value, CultureInfo.CurrentCulture));

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
        public IEnumerable<ClientModelValidationRule> AddValidation(ClientModelValidationContext context)
        {
            var adapt = new StringLengthAttributeAdapter(metadata, context, this);
            return adapt.GetClientValidationRules();

        }
    }
}
