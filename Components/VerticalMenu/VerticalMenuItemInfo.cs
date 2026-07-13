// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerticalMenuItemInfo.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 04/02/2016
//   Author:  Siddharth Sharma
//   Description: Vertical Menu's menu item information.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.VerticalMenu
{
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The menu item information class.
    /// </summary>
    public class VerticalMenuItemInfo
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the action url.
        /// </summary>
        public string ActionUrl { get; set; }

        /// <summary>
        /// Gets or sets the on click.
        /// </summary>
        public JRaw OnClick { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether causes validation.
        /// </summary>
        public bool CausesValidation { get; set; }
    }
}
