namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System;
    using Newtonsoft.Json;

    public class DataTableSortingJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) { return false; }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) { return null; }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { }
    }
}
