// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PageProvider.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the PageProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Infrastructure
{
    using System;
    using System.Web.UI;

    /// <summary>
    /// The page provider.
    /// </summary>
    public sealed class PageProvider
    {
        /// <summary>
        /// A Page object is required to fetch webresources
        /// As it is quite a big object it is lazyly cached
        /// </summary>
        private static readonly Lazy<Page> Lazy = new Lazy<Page>(() => new Page());

        /// <summary>
        /// Prevents a default instance of the <see cref="PageProvider"/> class from being created.
        /// </summary>
        private PageProvider()
        {
        }

        /// <summary>
        /// Gets the cached page.
        /// </summary>
        public static Page Instance
        {
            get
            {
                return Lazy.Value;
            }
        }
    }
}
