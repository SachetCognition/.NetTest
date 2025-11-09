using System.Collections.Generic;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid
{
    using System;

    /// <summary>
    /// The image text column.
    /// </summary>
    [Serializable]
    public class ImageTextColumn
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the  LSTIMAGE.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification =
            "TETHYS: This input is required."),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification =
            "TETHYS: The list values are to be provided by the user.")]
        public List<TreeGridimage> LstImage { get; set; }
    }
}
