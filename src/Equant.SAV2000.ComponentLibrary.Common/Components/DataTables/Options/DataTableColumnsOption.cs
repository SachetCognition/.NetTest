namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables.Options
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Represents a single column configuration for DataTables.
    /// </summary>
    public class DataTableColumnsOption
    {
        /// <summary>
        /// Gets or sets a value indicating whether the column is visible.
        /// </summary>
        [JsonProperty("bVisible")]
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for the column.
        /// </summary>
        [JsonProperty("sClass", NullValueHandling = NullValueHandling.Ignore)]
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets the column name.
        /// </summary>
        [JsonProperty("sName", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the column is sortable.
        /// </summary>
        [JsonProperty("bSortable")]
        public bool IsSortable { get; set; }

        /// <summary>
        /// Gets or sets the data source for the column.
        /// </summary>
        [JsonProperty("mData", NullValueHandling = NullValueHandling.Ignore)]
        public JRaw Data { get; set; }

        /// <summary>
        /// Gets or sets the render function for the column.
        /// </summary>
        [JsonProperty("mRender", NullValueHandling = NullValueHandling.Ignore)]
        public JRaw Render { get; set; }

        /// <summary>
        /// Gets or sets the default content for the column.
        /// </summary>
        [JsonProperty("sDefaultContent", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultContent { get; set; }

        /// <summary>
        /// Gets or sets the column type.
        /// </summary>
        [JsonProperty("sType", NullValueHandling = NullValueHandling.Ignore)]
        public string ColumnType { get; set; }
    }
}
