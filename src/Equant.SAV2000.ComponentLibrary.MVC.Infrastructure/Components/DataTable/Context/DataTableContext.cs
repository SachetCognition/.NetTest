namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    [Serializable]
    public class DataTableContext
    {
        public DataTableContext()
        {
            this.Sorting = null;
            this.DisplayStart = -1;
            this.SelectState = null;
        }

        public const string SortingKey = "Sorting";
        public const string DisplayStartKey = "DisplayStart";
        public const string SelectStateKey = "SelectState";

        [JsonProperty(SortingKey), JsonConverter(typeof(DataTableSortingJsonConverter))]
        public List<KeyValuePair<string, SortDirection>> Sorting { get; set; }

        [JsonProperty(DisplayStartKey)]
        public int DisplayStart { get; set; }

        [JsonProperty(SelectStateKey)]
        public Dictionary<string, bool> SelectState { get; set; }
    }
}
