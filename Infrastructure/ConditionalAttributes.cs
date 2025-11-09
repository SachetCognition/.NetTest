// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConditionalAttributes.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 25/11/2014
//   Author:  Sharma Siddharth (54626)
//   Description: Conditional Attributes base class for declaring conditional 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Infrastructure
{
    /// <summary>
    /// The conditional attributes.
    /// </summary>
    public class ConditionalAttributes
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalAttributes"/> class.
        /// </summary>
        public ConditionalAttributes()
        {
            this.IsVisible = false;
            this.IsUpdatable = false;
            this.IsMandatory = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether is visible.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is mandatory.
        /// </summary>
        public bool IsMandatory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is updateable.
        /// </summary>
        public bool IsUpdatable { get; set; }
    }
}
