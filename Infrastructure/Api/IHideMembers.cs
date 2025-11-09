// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHideMembers.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IHideMembers type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Infrastructure.Api
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Helper interface used to hide the base members from the fluent API to make it much cleaner 
    /// in Visual Studio IntelliSense.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IHideMembers
    {
        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object value);

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        /// <summary>
        /// The get type.
        /// </summary>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "GetType", Justification = "This should not be visible in auto complete list of VS, distracts when writing fluent syntax.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "In that case it is an issue of the .NET Framework itself")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();
    }
}
