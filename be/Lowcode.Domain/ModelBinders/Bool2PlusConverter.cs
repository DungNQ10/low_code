using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;

namespace Lowcode.Domain.ModelBinders
{
    public class Bool2PlusConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return System.Attribute.GetCustomAttributes(objectType).Any(v => v is bool);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (String.IsNullOrEmpty(reader.Value.ToString()))
                return null;

            if (reader.Value.ToString() == "+")
                return true;

            var value = default(decimal);
            var result = Decimal.TryParse(reader.Value.ToString(), NumberStyles.Any, new CultureInfo("vi-VN"), out value);
            return value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //use the default serialization - it works fine
            serializer.Serialize(writer, Convert.ToBoolean(value) == true ? "+" : "");
        }
    }
}