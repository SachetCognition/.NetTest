namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables.Options
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the ARIA accessibility language options for DataTables.
    /// </summary>
    public class DataTableLanguageAriaOption
    {
        /// <summary>
        /// Gets or sets the sort ascending label.
        /// </summary>
        [JsonProperty("sSortAscending", NullValueHandling = NullValueHandling.Ignore)]
        public string SortAscending { get; set; }

        /// <summary>
        /// Gets or sets the sort descending label.
        /// </summary>
        [JsonProperty("sSortDescending", NullValueHandling = NullValueHandling.Ignore)]
        public string SortDescending { get; set; }
    }
}
