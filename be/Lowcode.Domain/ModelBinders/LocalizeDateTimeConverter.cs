using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace Lowcode.Domain.ModelBinders
{
    public class LocalizeDateTimeConverter : IsoDateTimeConverter
    {
        public LocalizeDateTimeConverter()
        {
            base.DateTimeFormat = "dd-MM-yyyy";
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null || String.IsNullOrEmpty(reader.Value.ToString()))
                return null;

            return DateTime.Parse(reader.Value.ToString(), new CultureInfo("vi-VN"));
            //return base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }
}