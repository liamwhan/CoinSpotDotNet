using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoinSpotDotNet.Common
{
#pragma warning disable CS1591
    public class CoinSpotDateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => DateTime.ParseExact(reader.GetString(), "u", CultureInfo.InvariantCulture);

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }
#pragma warning restore CS1591
}
