namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables.Options
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the pagination language options for DataTables.
    /// </summary>
    public class DataTableLanguagePaginateOption
    {
        /// <summary>
        /// Gets or sets the first page label.
        /// </summary>
        [JsonProperty("sFirst", NullValueHandling = NullValueHandling.Ignore)]
        public string First { get; set; }

        /// <summary>
        /// Gets or sets the last page label.
        /// </summary>
        [JsonProperty("sLast", NullValueHandling = NullValueHandling.Ignore)]
        public string Last { get; set; }

        /// <summary>
        /// Gets or sets the next page label.
        /// </summary>
        [JsonProperty("sNext", NullValueHandling = NullValueHandling.Ignore)]
        public string Next { get; set; }

        /// <summary>
        /// Gets or sets the previous page label.
        /// </summary>
        [JsonProperty("sPrevious", NullValueHandling = NullValueHandling.Ignore)]
        public string Previous { get; set; }
    }
}
