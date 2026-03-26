namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables.Options
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the language/localization options for DataTables.
    /// </summary>
    public class DataTableLanguageOption
    {
        /// <summary>
        /// Gets or sets the empty table message.
        /// </summary>
        [JsonProperty("sEmptyTable", NullValueHandling = NullValueHandling.Ignore)]
        public string EmptyTable { get; set; }

        /// <summary>
        /// Gets or sets the info string.
        /// </summary>
        [JsonProperty("sInfo", NullValueHandling = NullValueHandling.Ignore)]
        public string Info { get; set; }

        /// <summary>
        /// Gets or sets the info empty string.
        /// </summary>
        [JsonProperty("sInfoEmpty", NullValueHandling = NullValueHandling.Ignore)]
        public string InfoEmpty { get; set; }

        /// <summary>
        /// Gets or sets the thousands separator for info.
        /// </summary>
        [JsonProperty("sInfoThousands", NullValueHandling = NullValueHandling.Ignore)]
        public string InfoThousands { get; set; }

        /// <summary>
        /// Gets or sets the loading records message.
        /// </summary>
        [JsonProperty("sLoadingRecords", NullValueHandling = NullValueHandling.Ignore)]
        public string LoadingRecords { get; set; }

        /// <summary>
        /// Gets or sets the pagination options.
        /// </summary>
        [JsonProperty("oPaginate", NullValueHandling = NullValueHandling.Ignore)]
        public DataTableLanguagePaginateOption Paginate { get; set; }

        /// <summary>
        /// Gets or sets the ARIA options.
        /// </summary>
        [JsonProperty("oAria", NullValueHandling = NullValueHandling.Ignore)]
        public DataTableLanguageAriaOption Aria { get; set; }

        /// <summary>
        /// Gets or sets the processing message.
        /// </summary>
        [JsonProperty("sProcessing", NullValueHandling = NullValueHandling.Ignore)]
        public string Processing { get; set; }

        /// <summary>
        /// Gets or sets the zero records message.
        /// </summary>
        [JsonProperty("sZeroRecords", NullValueHandling = NullValueHandling.Ignore)]
        public string ZeroRecords { get; set; }

        /// <summary>
        /// Gets or sets the info filtered string.
        /// </summary>
        [JsonProperty("sInfoFiltered", NullValueHandling = NullValueHandling.Ignore)]
        public string InfoFiltered { get; set; }
    }
}
