namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables.Options
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a column filter configuration for DataTables.
    /// </summary>
    public class DataTableColumnFilterColumnOption
    {
        /// <summary>
        /// Gets or sets the column filter type.
        /// </summary>
        [JsonProperty("type")]
        public DataTableColumnFilterColumnTypeOption ColumnType { get; set; }

        /// <summary>
        /// Gets or sets the max length for the filter input.
        /// </summary>
        [JsonProperty("maxLength", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxLength { get; set; }
    }
}
