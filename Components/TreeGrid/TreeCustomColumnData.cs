namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The tree custom column data.
    /// </summary>
    [Serializable]
    public  class TreeCustomColumnData
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        [JsonProperty("mData", NullValueHandling = NullValueHandling.Ignore)]
        public JRaw Data { get; set; }

    }
}
