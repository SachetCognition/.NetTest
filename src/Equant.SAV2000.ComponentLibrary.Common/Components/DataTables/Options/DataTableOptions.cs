namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables.Options
{
    using System.Collections.Generic;
    using System.Data;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Represents the main DataTable initialization options.
    /// </summary>
    public class DataTableOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether multi-select sorting is enabled.
        /// </summary>
        [JsonProperty("isMultiSelect", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsMultiSelect { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether server-side processing is enabled.
        /// </summary>
        [JsonProperty("bServerSide")]
        public bool IsServerSide { get; set; }

        /// <summary>
        /// Gets or sets the AJAX data source URL.
        /// </summary>
        [JsonProperty("sAjaxSource", NullValueHandling = NullValueHandling.Ignore)]
        public string AjaxSource { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether deferred rendering is enabled.
        /// </summary>
        [JsonProperty("bDeferRender")]
        public bool DeferRender { get; set; }

        /// <summary>
        /// Gets or sets the columns configuration.
        /// </summary>
        [JsonProperty("aoColumns", NullValueHandling = NullValueHandling.Ignore)]
        public List<DataTableColumnsOption> Columns { get; set; }

        /// <summary>
        /// Gets or sets the client-side data.
        /// </summary>
        [JsonProperty("aaData", NullValueHandling = NullValueHandling.Ignore)]
        public DataTable Data { get; set; }

        /// <summary>
        /// Gets or sets the DOM layout string.
        /// </summary>
        [JsonProperty("sDom", NullValueHandling = NullValueHandling.Ignore)]
        public string Dom { get; set; }

        /// <summary>
        /// Gets or sets the server request method.
        /// </summary>
        [JsonProperty("sServerMethod", NullValueHandling = NullValueHandling.Ignore)]
        public string ServerMethod { get; set; }

        /// <summary>
        /// Gets or sets the draw callback function.
        /// </summary>
        [JsonProperty("fnDrawCallback", NullValueHandling = NullValueHandling.Ignore)]
        public JRaw DrawCallBack { get; set; }

        /// <summary>
        /// Gets or sets the server params function.
        /// </summary>
        [JsonProperty("fnServerParams", NullValueHandling = NullValueHandling.Ignore)]
        public JRaw ServerParams { get; set; }

        /// <summary>
        /// Gets or sets the pagination type.
        /// </summary>
        [JsonProperty("sPaginationType", NullValueHandling = NullValueHandling.Ignore)]
        public string PaginationType { get; set; }

        /// <summary>
        /// Gets or sets the number of records per page.
        /// </summary>
        [JsonProperty("iDisplayLength")]
        public int DisplayLength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether filtering is enabled.
        /// </summary>
        [JsonProperty("bFilter")]
        public bool IsFilter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether pagination is enabled.
        /// </summary>
        [JsonProperty("bPaginate")]
        public bool IsPaginate { get; set; }

        /// <summary>
        /// Gets or sets the initial display start position.
        /// </summary>
        [JsonProperty("iDisplayStart")]
        public int DisplayStart { get; set; }

        /// <summary>
        /// Gets or sets the initial sorting configuration.
        /// </summary>
        [JsonProperty("aaSorting", NullValueHandling = NullValueHandling.Ignore)]
        public JRaw Sorting { get; set; }

        /// <summary>
        /// Gets or sets the horizontal scroll width.
        /// </summary>
        [JsonProperty("sScrollX", NullValueHandling = NullValueHandling.Ignore)]
        public string ScrollX { get; set; }

        /// <summary>
        /// Gets or sets the vertical scroll height.
        /// </summary>
        [JsonProperty("sScrollY", NullValueHandling = NullValueHandling.Ignore)]
        public string ScrollY { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether scroll collapse is enabled.
        /// </summary>
        [JsonProperty("bScrollCollapse")]
        public bool ScrollCollapse { get; set; }

        /// <summary>
        /// Gets or sets the column reorder options.
        /// </summary>
        [JsonProperty("oColReorder", NullValueHandling = NullValueHandling.Ignore)]
        public DataTableColumnsReorderOption ColReorder { get; set; }

        /// <summary>
        /// Gets or sets the language options.
        /// </summary>
        [JsonProperty("oLanguage", NullValueHandling = NullValueHandling.Ignore)]
        public DataTableLanguageOption Language { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether encryption is required.
        /// </summary>
        [JsonProperty("isEncryptionRequired")]
        public bool IsEncryptionRequired { get; set; }

        /// <summary>
        /// Gets or sets the encrypted parameters list.
        /// </summary>
        [JsonProperty("encryptedParameters", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> EncryptedParameters { get; set; }
    }
}
