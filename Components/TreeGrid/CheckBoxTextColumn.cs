namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid
{
    using System;

    /// <summary>
    /// The check box text column.
    /// </summary>
    [Serializable]
    public class CheckBoxTextColumn
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is visible.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is updatable.
        /// </summary>
        public bool IsUpdatable { get; set; }

        /// <summary>
        /// Gets or sets the text required for accessibility handling. 
        /// </summary>
        public string AccessText { get; set; }
    }
}
