using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;

namespace Lowcode.Domain.ModelBinders
{
    public class DecimalConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return System.Attribute.GetCustomAttributes(objectType).Any(v => v is decimal);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null || String.IsNullOrEmpty(reader.Value.ToString()))
                return null;

            var value = default(decimal);
            var result = Decimal.TryParse(reader.Value.ToString(), NumberStyles.Any, new CultureInfo("vi-VN"), out value);
            return value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //use the default serialization - it works fine
            value = System.Math.Ceiling((decimal)value * 100) / 100;
            writer.Culture = new CultureInfo("vi-VN");
            serializer.Serialize(writer, value);
        }
    }
}