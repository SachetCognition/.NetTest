using System;
using System.Collections.Generic;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public enum SortDirection
    {
        Asc = 0,
        Desc = 1
    }

    public class DataTableSortingJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var sorting = new List<KeyValuePair<string, SortDirection>>();
            foreach (var item in JArray.Load(reader))
            {
                var direction = item[1].Value<string>().Equals("desc") ? SortDirection.Desc : SortDirection.Asc;
                sorting.Add(new KeyValuePair<string, SortDirection>(item[0].Value<string>(), direction));
            }

            return sorting;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<KeyValuePair<string, SortDirection>>);
        }
    }
}
