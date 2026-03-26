namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables.Options
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the column reorder options for DataTables.
    /// </summary>
    public class DataTableColumnsReorderOption
    {
        /// <summary>
        /// Gets or sets a value indicating whether column reordering is allowed.
        /// </summary>
        [JsonProperty("allowReorder")]
        public bool AllowReorder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether column resizing is allowed.
        /// </summary>
        [JsonProperty("allowResize")]
        public bool AllowResize { get; set; }
    }
}
