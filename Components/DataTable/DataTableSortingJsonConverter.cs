using System;
using System.Collections.Generic;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The sorting json converter.
    /// </summary>
    public class DataTableSortingJsonConverter : JsonConverter
    {
        /// <summary>
        /// The write json.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="serializer">
        /// The serializer.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Not use, the serialization is done at client-side
            throw new NotImplementedException();
        }

        /// <summary>
        /// The read json.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="objectType">
        /// The object type.
        /// </param>
        /// <param name="existingValue">
        /// The existing value.
        /// </param>
        /// <param name="serializer">
        /// The serializer.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
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

        /// <summary>
        /// The can convert.
        /// </summary>
        /// <param name="objectType">
        /// The object type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<KeyValuePair<string, SortDirection>>);
        }
    }
}
