namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class TreeCustomColumnData
    {
        [JsonProperty("propertyName")]
        public string PropertyName { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public JRaw Data { get; set; }
    }
}
