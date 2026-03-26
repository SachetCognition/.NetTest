using System;
using System.ComponentModel.DataAnnotations;
namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
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
            if (!context.Attributes.ContainsKey("data-val"))
            {
                context.Attributes.Add("data-val", "true");
            }
            if (!context.Attributes.ContainsKey("data-val-length"))
            {
                context.Attributes.Add("data-val-length", ErrorMessage ?? string.Empty);
            }
            if (!context.Attributes.ContainsKey("data-val-length-max"))
            {
                context.Attributes.Add("data-val-length-max", this.MaximumLength.ToString());
            }
            if (this.MinimumLength > 0 && !context.Attributes.ContainsKey("data-val-length-min"))
            {
                context.Attributes.Add("data-val-length-min", this.MinimumLength.ToString());
            }
        }
    }
}
