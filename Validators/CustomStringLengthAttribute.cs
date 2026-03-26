using System;
using System.ComponentModel.DataAnnotations;
namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    using System.Collections.Generic;
    using System.Globalization;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-length", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-length-max", MaximumLength.ToString(CultureInfo.InvariantCulture));
            if (MinimumLength > 0)
            {
                MergeAttribute(context.Attributes, "data-val-length-min", MinimumLength.ToString(CultureInfo.InvariantCulture));
            }
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
