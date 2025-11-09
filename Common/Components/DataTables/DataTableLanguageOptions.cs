namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables
{
    /// <summary>
    /// Language options for DataTable
    /// </summary>
    public class DataTableLanguageOption
    {
        /// <summary>
        /// Gets or sets the search label
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Gets or sets the info text
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// Gets or sets the pagination options
        /// </summary>
        public DataTableLanguagePaginateOption Paginate { get; set; }

        /// <summary>
        /// Gets or sets the ARIA options
        /// </summary>
        public DataTableLanguageAriaOption Aria { get; set; }

        /// <summary>
        /// Gets or sets the empty table message
        /// </summary>
        public string EmptyTable { get; set; }

        /// <summary>
        /// Gets or sets the zero records message
        /// </summary>
        public string ZeroRecords { get; set; }

        /// <summary>
        /// Gets or sets the processing message
        /// </summary>
        public string Processing { get; set; }
    }

    /// <summary>
    /// Pagination language options
    /// </summary>
    public class DataTableLanguagePaginateOption
    {
        /// <summary>
        /// Gets or sets the first button text
        /// </summary>
        public string First { get; set; }

        /// <summary>
        /// Gets or sets the last button text
        /// </summary>
        public string Last { get; set; }

        /// <summary>
        /// Gets or sets the next button text
        /// </summary>
        public string Next { get; set; }

        /// <summary>
        /// Gets or sets the previous button text
        /// </summary>
        public string Previous { get; set; }
    }

    /// <summary>
    /// ARIA language options
    /// </summary>
    public class DataTableLanguageAriaOption
    {
        /// <summary>
        /// Gets or sets the sort ascending text
        /// </summary>
        public string SortAscending { get; set; }

        /// <summary>
        /// Gets or sets the sort descending text
        /// </summary>
        public string SortDescending { get; set; }
    }
}
