namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context
{
    using System;
    using System.Collections.Generic;

    using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;

    using Newtonsoft.Json;
    using SortDirection = Equant.SAV2000.ComponentLibrary.Common.Components.SortDirection;

    /// <summary>
    /// The data table context.
    /// </summary>
    [Serializable]
    public class DataTableContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTableContext"/> class.
        /// </summary>
        public DataTableContext()
        {
            this.Sorting = null;
            this.DisplayStart = -1;
            this.SelectState = null;
        }

        /// <summary>
        /// The context object sorting.
        /// </summary>
        public const string SortingKey = "Sorting";

        /// <summary>
        /// The context object display start.
        /// </summary>
        public const string DisplayStartKey = "DisplayStart";

        /// <summary>
        /// The context object select state.
        /// </summary>
        public const string SelectStateKey = "SelectState";

        /// <summary>
        /// Gets or sets the sorting.
        /// </summary>
        [JsonProperty(SortingKey), JsonConverter(typeof(DataTableSortingJsonConverter)),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists",
            Justification = "It is just a POCO used for serialization"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly",
            Justification = "It is just a POCO used for serialization")]
        public List<KeyValuePair<string, SortDirection>> Sorting { get; set; }

        /// <summary>
        /// Gets or sets the display start.
        /// </summary>
        [JsonProperty(DisplayStartKey)]
        public int DisplayStart { get; set; }

        /// <summary>
        /// Gets or sets the select state.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly",
             Justification = "It is just a POCO used for serialization"), JsonProperty(SelectStateKey)]
        public Dictionary<string, bool> SelectState { get; set; }

     }
}
