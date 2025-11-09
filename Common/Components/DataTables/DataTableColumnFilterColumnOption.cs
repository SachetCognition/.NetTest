namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables
{
    /// <summary>
    /// Options for DataTable column filter
    /// </summary>
    public class DataTableColumnFilterColumnOption
    {
        /// <summary>
        /// Gets or sets the column type
        /// </summary>
        public DataTableColumnFilterColumnTypeOption Type { get; set; }

        /// <summary>
        /// Gets or sets the column values
        /// </summary>
        public object Values { get; set; }
    }

    /// <summary>
    /// Column filter type options
    /// </summary>
    public enum DataTableColumnFilterColumnTypeOption
    {
        /// <summary>
        /// Text input filter
        /// </summary>
        Text,

        /// <summary>
        /// Select dropdown filter
        /// </summary>
        Select,

        /// <summary>
        /// Number input filter
        /// </summary>
        Number,

        /// <summary>
        /// Date input filter
        /// </summary>
        Date,

        /// <summary>
        /// Range filter
        /// </summary>
        Range,

        /// <summary>
        /// Checkbox filter
        /// </summary>
        Checkbox
    }
}
