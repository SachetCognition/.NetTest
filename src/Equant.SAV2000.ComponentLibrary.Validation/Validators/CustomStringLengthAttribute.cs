// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomStringLengthAttribute.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
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

    /// <summary>
    /// Specifies the minimum and maximum length of characters that are allowed in a data field
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class CustomStringLengthAttribute : StringLengthAttribute, IClientModelValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomStringLengthAttribute"/> class. 
        /// </summary>
        /// <param name="maximumLength">The maximum length.</param>
        public CustomStringLengthAttribute(int maximumLength)
            : base(maximumLength)
        {
        }

        /// <summary>
        /// Determines whether a specified object is valid.
        /// </summary>
        public override bool IsValid(object value)
        {
            return base.IsValid(Convert.ToString(value, CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Adds client-side validation attributes for unobtrusive validation.
        /// Replaces the StringLengthAttributeAdapter from .NET Framework.
        /// </summary>
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
