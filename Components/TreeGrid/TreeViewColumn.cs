namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid
{
    using System;

    /// <summary>
    /// The tree view column.
    /// </summary>
    [Serializable]
    public class TreeViewColumn
    {
        /// <summary>
        /// Gets or sets the header text.
        /// </summary>
        public string HeaderText { get; set; }

        /// <summary>
        /// Gets or sets the property name.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public ColumnType TreeColumnType { get; set; }

        /// <summary>
        /// Gets or sets the render.
        /// </summary>
        public string Render { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is header visible.
        /// </summary>
        public bool IsHeaderVisible { get; set; }

        /// <summary>
        /// Gets or sets the access text at column level.
        /// </summary>
        public string AccessText { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewColumn"/> class.
        /// </summary>
        public TreeViewColumn()
        {
            this.TreeColumnType =ColumnType.Text;
            IsHeaderVisible = true;
        }
    }

    /// <summary>
    /// The column type.
    /// </summary>
    public enum ColumnType
    {
        Text,
        Image,
        Custom,
        CheckBox
    };

}
