namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid
{
    using System;

    /// <summary>
    /// The TreeGridImage.
    /// </summary>
    [Serializable]
    public class TreeGridimage
    {
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the alt.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the action url.
        /// </summary>
        public string ActionUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is visible.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets the html attributes.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly",Justification="Tethys: Require to set this property from calling location so need to set its value")]
        public object HtmlAttributes { get;  set; }

    }
}
