using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Harcourts.eOpen.Web
{
    public class BoolConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((bool) value ? 1 : 0);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return string.Equals(reader.Value?.ToString(), "1", StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(bool) == objectType;
        }
    }

    public class StringConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((string) value ?? string.Empty);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return reader.Value?.ToString() ?? string.Empty;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (string) == objectType;
        }
    }
}