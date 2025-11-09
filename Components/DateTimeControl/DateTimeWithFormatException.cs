// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeWithFormatException.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 06/06/2014
//   Author:  Sharma Siddharth (54626)
//   Description: This class is for the exceptions which may occur while parsing the DateTimeWithFormat class using
//                Custom Model Binding. Such instances may occur when the user malciously tempers with the form data
//                which interferes with the model binding.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl
{
    using System;

    /// <summary>
    /// The date time with format exception.
    /// </summary>
    [Serializable]
    public class DateTimeWithFormatException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeWithFormatException"/> class.
        /// </summary>
        public DateTimeWithFormatException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeWithFormatException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public DateTimeWithFormatException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeWithFormatException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public DateTimeWithFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeWithFormatException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected DateTimeWithFormatException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
