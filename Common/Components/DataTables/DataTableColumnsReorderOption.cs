namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables
{
    /// <summary>
    /// Options for DataTable column reordering
    /// </summary>
    public class DataTableColumnsReorderOption
    {
        /// <summary>
        /// Gets or sets whether column reordering is enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets whether column reordering is allowed
        /// </summary>
        public bool AllowReorder { get; set; }

        /// <summary>
        /// Gets or sets whether column resizing is allowed
        /// </summary>
        public bool AllowResize { get; set; }

        /// <summary>
        /// Gets or sets the fixed columns count
        /// </summary>
        public int FixedColumnsLeft { get; set; }

        /// <summary>
        /// Gets or sets the fixed columns count on the right
        /// </summary>
        public int FixedColumnsRight { get; set; }
    }
}
